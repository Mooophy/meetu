using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeetU.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult TestNg()
        {
            return View();
        }

        public ActionResult Index()
        {
            return Redirect("Meetups/Index");
            //return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}