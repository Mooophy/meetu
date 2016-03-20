using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using MeetU.Models;
using Microsoft.AspNet.Identity;

namespace MeetU.API
{
    public class UsersController : ApiController
    {
        private readonly MuDbContext db = new MuDbContext();

        // GET: api/Users/5
        [ResponseType(typeof(UserViewModel))]
        public async Task<IHttpActionResult> Get(string userId)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var profile = await db.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
            // if neither presents
            if (user == null && profile == null)
            {
                return NotFound();
            }
            //  if only one presents, reply 500, indicating that the two tables doesn't match
            if (user == null || profile == null)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }

            var userView = new UserViewModel
            {
                UserId = userId,
                Email = user.Email,
                UserName = user.UserName,
                Number = user.Number,

                //this part can be updated by PUT.
                NickName = profile.NickName,
                GivenName = profile.GivenName,
                FamilyName = profile.FamilyName,
                Picture = profile.Picture,
                Gender = profile.Gender,

                CreatedAt = profile.CreatedAt,
                UpdatedAt = profile.UpdatedAt,
                LoginCount = profile.LoginCount
            };
            return Ok(userView);
        }

        //PUT: api/Users?
        public async Task<IHttpActionResult> Put(UserViewModel user)
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

            profile.Gender = user.FamilyName;
            profile.FamilyName = user.FamilyName;
            profile.GivenName = user.GivenName;
            profile.NickName = user.NickName;
            profile.Picture = user.Picture;
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
