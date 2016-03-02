//
//  This code can be used to do experiments on API
//

using System;
using System.Web.Http;
using MeetU.Models;

namespace MeetU.API
{
    public class TestsController : ApiController
    {
        private DbContext db = new DbContext();

        [Route("api/Developer/Scot/Now")]
        public DateTime GetTests()
        {
            return DateTime.Now;
        }
    }
}
