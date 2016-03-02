using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MeetU.Models;
using System.Net.Http.Headers;

namespace MeetU.API
{
    public class TestsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("api/Developer/Scot/Now")]
        public DateTime GetTests()
        {
            return DateTime.Now;
        }
    }
}
