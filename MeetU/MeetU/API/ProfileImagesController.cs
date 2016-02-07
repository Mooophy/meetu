using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MeetU.Models;
using System.Net.Http.Headers;

namespace MeetU.API
{
    public class ProfileImagesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProfileImages
        public IQueryable<ProfileImage> GetProfileImages()
        {
            return db.ProfileImages;
        }

        // GET: api/ProfileImages/?userId=some_id
        public HttpResponseMessage GetProfileImage(string userId)
        {
            var profileImage = db.ProfileImages.First(pi => pi.UserId == userId);

            var response = new HttpResponseMessage();
            if (profileImage == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }
            response.Content = new ByteArrayContent(profileImage.Image);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        // PUT: api/ProfileImages/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProfileImage(string id, ProfileImage profileImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != profileImage.UserId)
            {
                return BadRequest();
            }

            db.Entry(profileImage).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileImageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ProfileImages
        [ResponseType(typeof(ProfileImage))]
        public async Task<IHttpActionResult> PostProfileImage(ProfileImage profileImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProfileImages.Add(profileImage);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProfileImageExists(profileImage.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = profileImage.UserId }, profileImage);
        }

        // DELETE: api/ProfileImages/5
        [ResponseType(typeof(ProfileImage))]
        public async Task<IHttpActionResult> DeleteProfileImage(string id)
        {
            ProfileImage profileImage = await db.ProfileImages.FindAsync(id);
            if (profileImage == null)
            {
                return NotFound();
            }

            db.ProfileImages.Remove(profileImage);
            await db.SaveChangesAsync();

            return Ok(profileImage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfileImageExists(string id)
        {
            return db.ProfileImages.Count(e => e.UserId == id) > 0;
        }
    }
}