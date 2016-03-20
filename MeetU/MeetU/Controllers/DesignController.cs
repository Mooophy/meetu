//
//  This controller can be used to return the Design view for front end design or tests.
//  @Yue
//
using System.Web.Mvc;

namespace MeetU.Controllers
{
    public class DesignController : Controller
    {
        // GET: Design
        public ActionResult Index()
        {
            return View();
        }
    }
}