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

namespace MeetU.API
{
    public class ProfileDataUrisController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProfileDataUris
        public IQueryable<ProfileDataUri> GetProfileDataUri()
        {
            return db.ProfileDataUri;
        }

        // GET: api/ProfileDataUris/5
        [ResponseType(typeof(ProfileDataUri))]
        public async Task<IHttpActionResult> GetProfileDataUri(string id)
        {
            ProfileDataUri profileDataUri = await db.ProfileDataUri.FindAsync(id);
            if (profileDataUri == null)
            {
                return NotFound();
            }

            return Ok(profileDataUri);
        }

        // PUT: api/ProfileDataUris/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProfileDataUri(string id, ProfileDataUri profileDataUri)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != profileDataUri.UserId)
            {
                return BadRequest();
            }

            db.Entry(profileDataUri).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileDataUriExists(id))
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

        // POST: api/ProfileDataUris
        [ResponseType(typeof(ProfileDataUri))]
        public async Task<IHttpActionResult> PostProfileDataUri(ProfileDataUri profileDataUri)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProfileDataUri.Add(profileDataUri);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProfileDataUriExists(profileDataUri.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = profileDataUri.UserId }, profileDataUri);
        }

        // DELETE: api/ProfileDataUris/5
        [ResponseType(typeof(ProfileDataUri))]
        public async Task<IHttpActionResult> DeleteProfileDataUri(string id)
        {
            ProfileDataUri profileDataUri = await db.ProfileDataUri.FindAsync(id);
            if (profileDataUri == null)
            {
                return NotFound();
            }

            db.ProfileDataUri.Remove(profileDataUri);
            await db.SaveChangesAsync();

            return Ok(profileDataUri);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfileDataUriExists(string id)
        {
            return db.ProfileDataUri.Count(e => e.UserId == id) > 0;
        }
    }
}