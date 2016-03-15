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

        // GET: api/Users
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

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
                Pricture = profile.Pricture,
                Gender = profile.Gender,
                CreatedAt = profile.CreatedAt
            };

            return Ok(userView);
        }

        // POST: api/Users
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Users/5
        public void Delete(int id)
        {
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
