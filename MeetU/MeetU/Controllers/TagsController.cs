//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using MeetU.Models;
//using MeetU.Models.Domain;
//using Microsoft.AspNet.Identity;

//namespace MeetU.Controllers
//{
//    public class TagsController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        // GET: Tags
//        public async Task<ActionResult> Index()
//        {
//            var tags = db.Tags;
//            return View(await tags.ToListAsync());
//        }

//        // GET: Tags/Details/5
//        public async Task<ActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Tag tag = await db.Tags.FindAsync(id);
//            if (tag == null)
//            {
//                return HttpNotFound();
//            }
//            return View(tag);
//        }

//        //GET: Tags/Create
//        [Authorize]
//        public ActionResult Create()
//        {
//            //this.User.Identity.GetUserId();
//            //ViewBag.By = new SelectList(db.ApplicationUsers, "Id", "Email");
//            return View();
//        }

//        // POST: Tags/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [Authorize]
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create(Tag tag)
//        {
//            tag.By = User.Identity.GetUserId();
//            if (ModelState.IsValid)
//            {
//                db.Tags.Add(tag);
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }

//            //ViewBag.By = new SelectList(db.ApplicationUsers, "Id", "Email", tag.By);
//            return View(tag);
//        }


//        // GET: Tags/Edit/5
//        [Authorize]
//        public async Task<ActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Tag tag = await db.Tags.FindAsync(id);
//            if (tag == null)
//            {
//                return HttpNotFound();
//            }
//            //ViewBag.By = new SelectList(db.ApplicationUsers, "Id", "Email", tag.By);
//            return View(tag);
//        }

//        // POST: Tags/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [Authorize]
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Edit(Tag tag)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(tag).State = EntityState.Modified;
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }
//            //ViewBag.By = new SelectList(db.ApplicationUsers, "Id", "Email", tag.By);
//            return View(tag);
//        }

//        // GET: Tags/Delete/5
//        public async Task<ActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Tag tag = await db.Tags.FindAsync(id);
//            if (tag == null)
//            {
//                return HttpNotFound();
//            }
//            return View(tag);
//        }

//        // POST: Tags/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> DeleteConfirmed(int id)
//        {
//            Tag tag = await db.Tags.FindAsync(id);
//            db.Tags.Remove(tag);
//            await db.SaveChangesAsync();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
