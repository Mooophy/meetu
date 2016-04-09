using System.Linq;
using System.Web.Http;
using MeetU.Models;

namespace MeetU.API
{
    public class UsersDiffProfilesView
    {
        public IQueryable<UserNotInProfiles> UserIds { get; set; }
        public IQueryable<ProfileNotInUsers> ProfileIds { get; set; }
    }
    public class DbMonitorController : ApiController
    {
        private readonly MuDbContext db = new MuDbContext();

        [Route("api/DbMonitor/UsersDiffProfiles")]
        public UsersDiffProfilesView GetUsersDiffProfiles()
        {
            return new UsersDiffProfilesView
            {
                UserIds = db.UsersNotInProfiles,
                ProfileIds = db.ProfilesNotInUsers
            };
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
