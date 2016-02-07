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

        [Route("api/Test/Image")]
        public HttpResponseMessage GetImage()
        {
            var profileImage = db.ProfileImages
                .FirstOrDefault(
                    p => p.UserId == "21dad012-aa70-4d43-b824-5fa7a94d0f82"
                );

            var response = new HttpResponseMessage();
            response.Content = new ByteArrayContent(profileImage.Image);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/");
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
    }
}
