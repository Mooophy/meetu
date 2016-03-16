using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MeetU.Models;

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
            if (user == null || profile == null)
                return NotFound();

            var userView = new UserViewModel
            {
                UserId = userId,
                Email = user.Email,
                UserName = user.UserName,
                Number = user.Number,

                NickName = profile.NickName,
                GivenName = profile.GivenName,
                FamilyName = profile.FamilyName,
                Pricture = profile.Picture,
                Gender = profile.Gender,
                CreatedAt = profile.CreatedAt
            };

            return Ok(userView);
        }

        //PUT: api/Users?
       //[ResponseType(typeof(void))]
       // public async Task<IHttpActionResult> Put(string userId, UserViewModel user)
       // {
       //     var profile = await db.Profiles.FirstOrDefaultAsync(u => u.UserId == user.UserId);
       //     if (profile == null)
       //     {
       //         return NotFound();
       //     }

       //     profile.FamilyName = user.FamilyName;
       //     profile.GivenName = user.GivenName;
       //     profile.NickName = user.NickName;
       //     profile.Picture = user.Pricture;
       //     profile.Gender = user.Gender;

       //     db.Entry(profile).State = EntityState.Modified;
       //     await db.SaveChangesAsync();
       // }

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
