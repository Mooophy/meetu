using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace MeetU.API
{
    public class LoggedUserController : ApiController
    {
        public IQueryable<string> GetLoggedUser()
        {
            var result = new List<string> { User.Identity.GetUserId() };
            return result.AsQueryable();
        }
    }
}