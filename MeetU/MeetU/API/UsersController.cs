using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Net;
using System.Web.Http;
using MeetU.Models;
using Microsoft.AspNet.Identity;

namespace MeetU.API
{
    public class UsersController : ApiController
    {
        private readonly MuDbContext db = new MuDbContext();

        // GET: api/Users/5
        public async Task<IHttpActionResult> Get(string userId)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var profile = await db.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
            // if neither presented
            if (user == null && profile == null)
            {
                return NotFound();
            }
            //  if only one presents, reply 500, indicating that the two tables doesn't match
            if (user == null || profile == null)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }

            var userView = new PrivateUserViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Number = user.Number,

                NickName = profile.NickName,
                Picture = profile.Picture,
                Gender = profile.Gender,
                Brief = profile.Brief,

                CreatedAt = profile.CreatedAt,
                UpdatedAt = profile.UpdatedAt
            };
            if (userId != User.Identity.GetUserId())
            {
                return Ok(userView as PublicUserViewModel);
            }

            //only when user is trying to view his own profile
            userView.GivenName = profile.GivenName;
            userView.FamilyName = profile.FamilyName;
            userView.LoginCount = profile.LoginCount;
            return Ok(userView);
        }

        //PUT: api/Users?
        public async Task<IHttpActionResult> Put(PrivateUserViewModel user)
        {
            if (user.UserId != User.Identity.GetUserId())
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            var profile = await db.Profiles.FirstOrDefaultAsync(p => p.UserId == user.UserId);
            if (profile == null)
            {
                return NotFound();
            }

            profile.Gender = user.Gender;
            profile.FamilyName = user.FamilyName;
            profile.GivenName = user.GivenName;
            profile.NickName = user.NickName;
            profile.Picture = user.Picture;
            profile.Brief = user.Brief;
            profile.UpdatedAt = DateTime.Now;

            db.Entry(profile).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
