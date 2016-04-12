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
    public class FollowsController : ApiController
    {
        private MuDbContext db = new MuDbContext();

        // GET: api/Follows
        public IQueryable<Follow> GetFollows()
        {
            return db.Follows;
        }

        // GET: api/Follows/5
        //[ResponseType(typeof(Follow))]
        //public async Task<IHttpActionResult> GetFollow(string id)
        //{
        //    Follow follow = await db.Follows.FindAsync(id);
        //    if (follow == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(follow);
        //}

        // PUT: api/Follows/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFollow(string id, Follow follow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != follow.FollowedUserId)
            {
                return BadRequest();
            }

            db.Entry(follow).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowExists(id))
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

        // POST: api/Follows
        [ResponseType(typeof(Follow))]
        public async Task<IHttpActionResult> PostFollow(Follow follow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Follows.Add(follow);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FollowExists(follow.FollowedUserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = follow.FollowedUserId }, follow);
        }

        // DELETE: api/Follows/5
        //[ResponseType(typeof(Follow))]
        //public async Task<IHttpActionResult> DeleteFollow(string id)
        //{
        //    Follow follow = await db.Follows.FindAsync(id);
        //    if (follow == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Follows.Remove(follow);
        //    await db.SaveChangesAsync();

        //    return Ok(follow);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FollowExists(string id)
        {
            return db.Follows.Count(e => e.FollowedUserId == id) > 0;
        }
    }
}