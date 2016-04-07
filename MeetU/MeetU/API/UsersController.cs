using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Http;
using MeetU.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Infrastructure;

namespace MeetU.API
{
    public class UsersController : ApiController
    {
        private readonly MuDbContext db = new MuDbContext();
        private string LoggedInUserId => User.Identity.GetUserId();

        [Route("api/Users/Public")]
        public async Task<IHttpActionResult> GetPublicProfile(string userId, int joinedAmount, int launchedAmount)
        {
            var user = await db
                .Users
                .FirstOrDefaultAsync(u => u.Id == userId);
            var profile = await db
                .Profiles
                .FirstOrDefaultAsync(p => p.UserId == userId);
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

            var publicUserView = new PublicUserViewModel()
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

            var JoinedMeetupIds = db.Joins.Where(j => j.UserId == user.Id);
            publicUserView.JoinedMeetups = db
                .Meetups
                .Where(m => JoinedMeetupIds.Count(j => j.MeetupId == m.Id) > 0)
                .Take(joinedAmount);
            publicUserView.JoinedMeetupsTotal = db
                .Meetups
                .Where(m => JoinedMeetupIds.Count(j => j.MeetupId == m.Id) > 0)
                .Count();
            publicUserView.LaunchedMeetups = db
                .Meetups
                .Where(m => m.Sponsor == userId)
                .Take(launchedAmount);
            publicUserView.LaunchedMeetupsTotal = db
                .Meetups
                .Where(m => m.Sponsor == user.Id)
                .Count();

            return Ok(publicUserView);
        }

        [Route("api/Users/Private")]
        public async Task<IHttpActionResult> GetPrivateProfile(string userId, int joinedAmount, int launchedAmount)
        {
            if (userId != LoggedInUserId)
            {
                return Content(HttpStatusCode.Forbidden, "Trying to get forbidden info -- Meet.u");
            }

            var user = await db
                .Users
                .FirstOrDefaultAsync(u => u.Id == userId);
            var profile = await db
                .Profiles
                .FirstOrDefaultAsync(p => p.UserId == userId);
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

            var privateUserView = new PrivateUserViewModel()
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
                UpdatedAt = profile.UpdatedAt,
                //private part
                FamilyName = profile.FamilyName,
                GivenName = profile.GivenName,
                LoginCount = profile.LoginCount
            };

            var JoinedMeetupIds = db.Joins.Where(j => j.UserId == user.Id);
            privateUserView.JoinedMeetups = db
                .Meetups
                .Where(m => JoinedMeetupIds.Count(j => j.MeetupId == m.Id) > 0)
                .Take(joinedAmount);
            privateUserView.JoinedMeetupsTotal = db
                .Meetups
                .Where(m => JoinedMeetupIds.Count(j => j.MeetupId == m.Id) > 0)
                .Count();
            privateUserView.LaunchedMeetups = db
                .Meetups
                .Where(m => m.Sponsor == userId)
                .Take(launchedAmount);
            privateUserView.LaunchedMeetupsTotal = db
                .Meetups
                .Where(m => m.Sponsor == user.Id)
                .Count();

            return Ok(privateUserView);
        }

        //PUT: api/Users?
        [Route("api/Users/Private")]
        [HttpPut]
        public async Task<IHttpActionResult> Put(PrivateUserViewModel user)
        {
            if (user.UserId != LoggedInUserId)
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
