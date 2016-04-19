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
    public class FollowingOrFollowedView
    {
        public string UserId { get; set; }
        public string NickName { get; set; }
        public string Picture { get; set; }
        public DateTime At { get; set; }
    }

    public class FollowsController : ApiController
    {
        private MuDbContext db = new MuDbContext();

        // GET: api/Follows
        public IQueryable<Follow> GetFollows()
        {
            return db.Follows;
        }

        // GET: api/Following?userId=some-userId
        [HttpGet]
        [Route("api/Following")]
        public IQueryable<FollowingOrFollowedView> GetProfileByFollowingUserId(string userId)
        {
            return
                from f in db.Follows
                where f.FollowingUserId == userId
                join p in db.Profiles
                on f.FollowedUserId equals p.UserId
                select new FollowingOrFollowedView
                {
                    UserId = p.UserId,
                    NickName = p.NickName,
                    Picture = p.Picture,
                    At = f.At
                };
        }

        //GET: api/FollowedBy?userId=some-userId
        [HttpGet]
        [Route("api/FollowedBy")]
        public IQueryable<FollowingOrFollowedView> GetProfileByFollowedUserId(string userId)
        {
            return
                from f in db.Follows
                where f.FollowedUserId == userId
                join p in db.Profiles
                on f.FollowingUserId equals p.UserId
                select new FollowingOrFollowedView
                {
                    UserId = p.UserId,
                    NickName = p.NickName,
                    Picture = p.Picture,
                    At = f.At
                };
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

        //DELETE: api/Follows/5
        [ResponseType(typeof(Follow))]
        public async Task<IHttpActionResult> DeleteFollow(string followedUserId, string followingUserId)
        {
            if (followingUserId != User.Identity.GetUserId())
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            var follow = db.Follows.Find(followedUserId, followingUserId);
            if (follow == null)
            {
                return NotFound();
            }

            db.Follows.Remove(follow);
            await db.SaveChangesAsync();
            return Ok(follow);
        }

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