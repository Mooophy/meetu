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
    public class JoinsController : ApiController
    {
        private Models.DbContext db = new Models.DbContext();

        // GET: api/Joins
        public IQueryable<Join> GetJoins()
        {
            return db.Joins;
        }

        // GET: api/Joins/5
        [ResponseType(typeof(Join))]
        public async Task<IHttpActionResult> GetJoin(int id)
        {
            Join join = await db.Joins.FindAsync(id);
            if (join == null)
            {
                return NotFound();
            }

            return Ok(join);
        }

        // PUT: api/Joins/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutJoin(int id, Join join)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != join.MeetupId)
            {
                return BadRequest();
            }

            db.Entry(join).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JoinExists(id))
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

        // POST: api/Joins
        [ResponseType(typeof(Join))]
        public async Task<IHttpActionResult> PostJoin(Join join)
        {
            join.At = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Joins.Add(join);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JoinExists(join.MeetupId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = join.MeetupId }, join);
        }

        // DELETE: api/Joins/5
        [ResponseType(typeof(Join))]
        public async Task<IHttpActionResult> DeleteJoin(int meetupId, string userId)
        {
            Join join = await db.Joins.FindAsync(meetupId, userId);
            if (join == null)
            {
                return NotFound();
            }

            db.Joins.Remove(join);
            await db.SaveChangesAsync();

            return Ok(join);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JoinExists(int id)
        {
            return db.Joins.Count(e => e.MeetupId == id) > 0;
        }
    }
}