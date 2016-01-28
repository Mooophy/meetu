using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MeetU.Models;
using System.Net.Http.Headers;

namespace MeetU.API
{
    //
    //  This class is used for experiments and tests.
    //
    public class TestController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //GET: api/test
        public HttpResponseMessage GetTests()
        {
            //
            //  Code to fetch the image from db and then send it back to client.
            //  Can be displayed by Browser.
            //
            var profileImage = db.ProfileImages
                .FirstOrDefault(
                    p => p.UserId == "21dad012-aa70-4d43-b824-5fa7a94d0f82"
                );

            var response = new HttpResponseMessage();
            response.Content = new ByteArrayContent(profileImage.Image);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            response.StatusCode = HttpStatusCode.OK;
            return response;

            //
            // code to save image into database
            //
            //var profile = new ProfileImage
            //{
            //    UserId = "21dad012-aa70-4d43-b824-5fa7a94d0f82",
            //    Image = System.IO.File.ReadAllBytes(@"C:\pusheencat.png")
            //};
            //var msg = "";
            //try
            //{
            //    db.ProfileImages.Add(profile);
            //    db.SaveChanges();
            //}
            //catch (Exception e)
            //{
            //    msg = e.Message;
            //}
            //return new List<string> { msg }.AsQueryable();
        }
    }
}
