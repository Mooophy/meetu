using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace MeetU.API
{
    public class TestsController : ApiController
    {
        [Route("api/Developer/Scot/Now")]
        public DateTime GetTests()
        {
            return DateTime.Now;
        }
    }
}
