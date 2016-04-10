using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MeetU.Models;
using Microsoft.AspNet.Identity;

namespace MeetU.API
{
    [Authorize]
    public class MeetupsController : ApiController
    {
        private MuDbContext db = new MuDbContext();

        [Route("api/Meetups/LaunchedBy")]
        public IHttpActionResult GetMeetupsLaunchedBy(string userId)
        {
            return Ok(db
                .Meetups
                .Where(m => m.Sponsor == userId)
                .Select(
                    m => new MeetupViewModel
                    {
                        Meetup = m,
                        SponsorUserName = m.ApplicationUser.UserName,
                        SponsorNickName = (db.Profiles.FirstOrDefault(p => p.UserId == m.Sponsor)).NickName,
                        Joins = db.Joins.Where(j => j.MeetupId == m.Id)
                    }
                )
            );
        }

        // GET: api/Meetups
        // note: this controller returns an array of MeetupViewModels to front end, rather than array of Meetup.
        public IQueryable<MeetupViewModel> GetMeetups()
        {
            var meetups =
                db.Meetups
                .Where(m => m.IsCancelled == false)
                .OrderByDescending(m => m.CreatedAt)
                .Select(
                m => new MeetupViewModel
                {
                    Meetup = m,
                    SponsorUserName = m.ApplicationUser.UserName,
                    Joins = db.Joins.Where(j => j.MeetupId == m.Id)
                });

            //append user name
            foreach (var m in meetups)
                foreach (var j in m.Joins)
                    if (j.UserName == null)
                        j.UserName = j.ApplicationUser.UserName;

            return meetups;
        }

        // GET: api/Meetups?start=the-starting-index&amount=how-many-to-fetch
        // note: this controller returns an array of MeetupViewModels to front end, rather than array of Meetup.
        // To be abstact together with the api controller: GetMeetups() 
        public IQueryable<MeetupViewModel> GetMeetups(int start, int amount)
        {
            var pagedMeetups =
                db.Meetups
                .Where(m => m.IsCancelled == false)
                .OrderByDescending(m => m.CreatedAt)
                .Skip(start)
                .Take(amount)
                .Select(
                m => new MeetupViewModel
                {
                    Meetup = m,
                    SponsorUserName = m.ApplicationUser.UserName,
                    Joins = db.Joins.Where(j => j.MeetupId == m.Id)
                });

            foreach (var m in pagedMeetups)
                foreach (var j in m.Joins)
                    if (j.UserName == null)
                        j.UserName = j.ApplicationUser.UserName;

            return pagedMeetups;
        }

        // GET: api/Meetups/5
        [ResponseType(typeof(Meetup))]
        public async Task<IHttpActionResult> GetMeetup(int id)
        {
            Meetup meetup = await db.Meetups
                .Where(m => m.IsCancelled == false)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (meetup == null)
            {
                return NotFound();
            }
            return Ok(meetup);
        }

        // PUT: api/Meetups/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMeetup(Meetup meetup)
        {
            // Ensure it's post by sponser
            if (meetup.Sponsor != User.Identity.GetUserId())
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            // Ensure valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ensure existing
            var old = await db.Meetups.FirstOrDefaultAsync(m => m.Id == meetup.Id);
            if (old == null)
            {
                return NotFound();
            }

            // Update and save
            old.Description = meetup.Description;
            old.Title = meetup.Title;
            old.When = meetup.When;
            old.Where = meetup.Where;
            meetup.UpdatedAt = DateTime.Now;
            db.Entry(old).State = EntityState.Modified;
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

        // POST: api/Meetups
        [ResponseType(typeof(Meetup))]
        public async Task<IHttpActionResult> PostMeetup(Meetup meetup)
        {
            if (meetup.Sponsor != User.Identity.GetUserId())
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            meetup.IsCancelled = false;
            meetup.CreatedAt = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Meetups.Add(meetup);
            await db.SaveChangesAsync();
            return CreatedAtRoute("DefaultApi", new { id = meetup.Id }, meetup);
        }

        // DELETE: api/Meetups/5
        [ResponseType(typeof(Meetup))]
        public async Task<IHttpActionResult> DeleteMeetup(int id)
        {
            Meetup meetup =
                await db.Meetups
                .Where(m => m.IsCancelled == false)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (meetup == null)
                return NotFound();
            if (meetup.Sponsor != User.Identity.GetUserId())
                return StatusCode(HttpStatusCode.Forbidden);
            meetup.IsCancelled = true;
            meetup.CancelledAt = DateTime.Now;
            await db.SaveChangesAsync();

            return Ok(meetup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MeetupExists(int id)
        {
            return db.Meetups.Count(e => e.Id == id) > 0;
        }
    }
}