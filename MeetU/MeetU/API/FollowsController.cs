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
using Microsoft.AspNet.Identity;
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

        // POST: api/Follows
        [ResponseType(typeof(Follow))]
        public async Task<IHttpActionResult> PostFollow(Follow follow)
        {
            if (follow.FollowingUserId != User.Identity.GetUserId())
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            follow.At = DateTime.Now;
            if (!ModelState.IsValid || follow.FollowingUserId == follow.FollowedUserId)
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