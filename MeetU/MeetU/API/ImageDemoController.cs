﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MeetU.API
{
    public class ImageDemoController : ApiController
    {
        // GET: api/ImageDemo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ImageDemo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ImageDemo
        public void Post(dynamic paras)
        {
            var len = paras.dataUri;
        }

        // PUT: api/ImageDemo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ImageDemo/5
        public void Delete(int id)
        {
        }
    }
}
