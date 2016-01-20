using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MeetU.Models;
using Microsoft.AspNet.Identity;

namespace MeetU.Controllers
{
    public class JoinsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Joins
        //public async Task<ActionResult> Index()
        //{
        //    var joins = db.Joins.Include(j => j.ApplicationUser).Include(j => j.Meetup);
        //    return View(await joins.ToListAsync());
        //}

        //// GET: Joins/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Join join = await db.Joins.FindAsync(id);
        //    if (join == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(join);
        //}

        //// GET: Joins/Create
        //public ActionResult Create()
        //{
        //    ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email");
        //    ViewBag.MeetupId = new SelectList(db.Meetups, "Id", "Title");
        //    return View();
        //}

        // GET: Joins/Join
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        public async Task<ActionResult> Join(int meetupId)
        {
            var userId = User.Identity.GetUserId();
            if (null == db.Joins.Find(meetupId, userId))
            {
                var join = new Join
                {
                    MeetupId = meetupId,
                    UserId = User.Identity.GetUserId(),
                    At = DateTime.Now
                };
                //
                //  Seems need check the model object if valid first.
                //
                db.Joins.Add(join);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Details", "Meetups", new { Id = meetupId });
        }

        [Authorize]
        public async Task<ActionResult> Unjoin(int meetupId)
        {
            var userId = User.Identity.GetUserId();
            var join = await db.Joins.FindAsync(meetupId, userId);
            if (null != join)
            {
                db.Joins.Remove(join);
            }
            await db.SaveChangesAsync();
            //var userId = User.Identity.GetUserId();
            //if (null != db.Joins.Find(meetupId, userId))
            //{
            //    var join = new Join
            //    {
            //        MeetupId = meetupId,
            //        UserId = User.Identity.GetUserId(),
            //        At = DateTime.Now
            //    };
            //    //
            //    //  Seems need check the model object if valid first.
            //    //
            //    db.Joins.Add(join);
            //    await db.SaveChangesAsync();
            //}
            return RedirectToAction("Details", "Meetups", new { Id = meetupId });
        }

        //// GET: Joins/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Join join = await db.Joins.FindAsync(id);
        //    if (join == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", join.UserId);
        //    ViewBag.MeetupId = new SelectList(db.Meetups, "Id", "Title", join.MeetupId);
        //    return View(join);
        //}

        //// POST: Joins/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,MeetupId,UserId,At")] Join join)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(join).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", join.UserId);
        //    ViewBag.MeetupId = new SelectList(db.Meetups, "Id", "Title", join.MeetupId);
        //    return View(join);
        //}

        //// GET: Joins/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Join join = await db.Joins.FindAsync(id);
        //    if (join == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(join);
        //}

        //// POST: Joins/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Join join = await db.Joins.FindAsync(id);
        //    db.Joins.Remove(join);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
