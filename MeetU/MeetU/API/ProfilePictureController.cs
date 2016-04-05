using System;
using System.Net;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using MeetU.Lib;
using MeetU.Models;

namespace MeetU.API
{
    public class ProfilePictureController : ApiController
    {
        public class ProfilePicture
        {
            public string UserId { get; set; }
            public string Data { get; set; }
        }

        // GET: api/ProfilePicture/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ProfilePicture
        public async Task<IHttpActionResult> Post(ProfilePicture profilePicture)
        {
            if (profilePicture.UserId != User.Identity.GetUserId())
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            var dataUri = new DataUri(profilePicture.Data);
            if (!dataUri.IsSupported)
            {
                return BadRequest();
            }

            var objectName = String.Format("{0}.{1}", profilePicture.UserId, dataUri.Format);
            var finalUrl = @"https://s3-ap-southeast-2.amazonaws.com/meet.u/ProfilePictures/" + objectName;
            var deferred = Task
                .Factory
                .StartNew(
                    () => 
                        UploadToS3Async(profilePicture.UserId, dataUri, objectName)
                );
            await deferred
                .ContinueWith(
                    d => 
                        UpdateProfileTableAsync(profilePicture.UserId, finalUrl), TaskContinuationOptions.OnlyOnRanToCompletion
                );

            return Ok(finalUrl);
        }

        // DELETE: api/ProfilePicture/5
        public void Delete(int id)
        {
        }

        #region  helpers
        async Task UploadToS3Async(string userId, DataUri dataUri, string objectName)
        {
            using (IAmazonS3 client = new AmazonS3Client(region: RegionEndpoint.APSoutheast2))
            {
                var request = new TransferUtilityUploadRequest
                {
                    BucketName = "meet.u",
                    Key = "ProfilePictures/" + objectName,
                    InputStream = dataUri.ToStream
                };
                await new TransferUtility(client).UploadAsync(request);
            }
        }

        async Task UpdateProfileTableAsync(string userId, string pictureUrl)
        {
            using (var db = new MuDbContext())
            {
                db.Profiles.Find(userId).Picture = pictureUrl;
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        #endregion
    }
}
