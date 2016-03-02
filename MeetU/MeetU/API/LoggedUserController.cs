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

        public class UserView
        {
            public string UserId { get; set; }
            public string UserName { get; set; }
        }

        public IQueryable<UserView> GetLoggedUser()
        {
            var result = new List<UserView>
            {
                new UserView
                {
                    UserId = User.Identity.GetUserId(),
                    UserName = User.Identity.GetUserName()
                }
            };
            return result.AsQueryable();
        }
    }
}