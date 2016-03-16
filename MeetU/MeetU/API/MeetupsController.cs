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

        // GET: api/Meetups
        // note: this controller returns an array of MeetupViewModels to front end, rather than array of Meetup.
        public IQueryable<MeetupViewModel> GetMeetups()
        {
            var meetups =
                db.Meetups
                .Where(m => m.IsCancelled == false)
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
                .OrderByDescending(m => m.Date)
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
            Meetup meetup = 
                await db.Meetups
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
        public async Task<IHttpActionResult> PutMeetup(int id, Meetup meetup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meetup.Id)
            {
                return BadRequest();
            }

            db.Entry(meetup).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetupExists(id))
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

        // POST: api/Meetups
        [ResponseType(typeof(Meetup))]
        public async Task<IHttpActionResult> PostMeetup(Meetup meetup)
        {
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
            if(meetup.Sponsor != User.Identity.GetUserId())
                return BadRequest();
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