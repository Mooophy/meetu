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
    //
    //  the view model of domain model Comment, 
    //  as JSON at front end.
    //  @Yue
    //
    public class CommentView
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string By { get; set; }
        public int MeetupId { get; set; }
        public DateTime At { get; set; }
    }

    public class CommentsController : ApiController
    {
        private MuDbContext db = new MuDbContext();

        //GET: api/Comments
        public IQueryable<CommentView> GetComments()
        {
            return
                from c in db.Comments
                select new CommentView
                {
                    Id = c.Id,
                    Content = c.Content,
                    By = c.ApplicationUser.UserName,
                    MeetupId = c.MeetupId,
                    At = c.At
                };
        }

        //GET: api/comments?meetupId=some-meetupId
        public IQueryable<CommentView> GetComments(int meetupId)
        {
            return
                from c in db.Comments
                where c.MeetupId == meetupId
                select new CommentView
                {
                    Id = c.Id,
                    Content = c.Content,
                    By = c.ApplicationUser.UserName,
                    MeetupId = c.MeetupId,
                    At = c.At
                };
        }

        // GET: api/Comments/5
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> GetComment(int id)
        {
            Comment comment = await db.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // PUT: api/Comments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutComment(int id, Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.Id)
            {
                return BadRequest();
            }

            db.Entry(comment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/Comments
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> PostComment(Comment comment)
        {
            comment.At = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comments.Add(comment);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comments/5
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> DeleteComment(int id)
        {
            var comment = await db.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null)
            {
                return NotFound();
            }
            //
            //  To check if logged user is comment's author
            //  @Yue
            //
            if (comment.By != User.Identity.GetUserId())
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            db.Comments.Remove(comment);
            await db.SaveChangesAsync();

            return Ok(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(int id)
        {
            return db.Comments.Count(e => e.Id == id) > 0;
        }
    }
}