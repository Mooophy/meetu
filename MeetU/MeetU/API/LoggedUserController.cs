using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MeetU.Models;

namespace MeetU.API
{
    public class LoggedUserController : ApiController
    {
        private MuDbContext db = new MuDbContext();

        public class LoggedInUserView
        {
            public string UserId { get; set; }
            public string UserName { get; set; }
        }

        public IQueryable<LoggedInUserView> GetLoggedUser()
        {
            var result = new List<LoggedInUserView>
            {
                new LoggedInUserView
                {
                    UserId = User.Identity.GetUserId(),
                    UserName = User.Identity.GetUserName()
                }
            };
            return result.AsQueryable();
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