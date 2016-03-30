using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.S3.Model;
using MeetU.Lib;


namespace MeetU.API
{
    public class ImageDemoController : ApiController
    {

        // GET: api/ImageDemo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ImageDemo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ImageDemo
        public async Task<IHttpActionResult> Post(dynamic paras)
        {
            var dataUri = new DataUri((string)paras.dataUri);
            if (!dataUri.IsSupported)
            {
                return BadRequest();
            }

            string objectName = String.Format("{0}.{1}", DateTime.Now.Ticks.ToString(), dataUri.Format);
            using (IAmazonS3 client = new AmazonS3Client(awsAccessKeyId: "AKIAJSG5URXPSALPTZKQ", awsSecretAccessKey: "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", region: RegionEndpoint.APSoutheast2))
            {
                var request = new TransferUtilityUploadRequest
                {
                    BucketName = "meet.u",
                    Key = objectName,
                    InputStream = dataUri.ToStream
                };
                await new TransferUtility(client).UploadAsync(request);
            }

            return Ok(@"https://s3-ap-southeast-2.amazonaws.com/meet.u/" + objectName);
        }

        // PUT: api/ImageDemo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ImageDemo/5
        public void Delete(int id)
        {
        }
    }

}
