using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using MeetU.Models;
using Microsoft.AspNet.Identity;

namespace MeetU.Controllers
{
    [Authorize]
    public class MeetupsController : Controller
    {
        private MuDbContext db = new MuDbContext();

        // GET: Meetups
        public ActionResult Index() => View();

        // GET: Meetups/Create
        [Authorize]
        public ActionResult Create() => View();

        // POST: Meetups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Meetup meetup)
        {
            meetup.Sponsor = User.Identity.GetUserId();
            meetup.CreatedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Meetups.Add(meetup);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(meetup);
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
