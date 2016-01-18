using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeetU.Controllers
{
    public class DesignController : Controller
    {
        // GET: Design
        public string Index()
        {
            return "<h1>For Design and Testing.</h1>";
        }

        // GET: Design/DateTimePicker
        public ActionResult DateTimePicker()
        {
            return View();
        }

    }
}