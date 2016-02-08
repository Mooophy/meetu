using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetU.Models;

namespace TestCSharpPart
{
    /// <summary>
    /// Summary description for ExerimentsOnDatabase
    /// </summary>
    [TestClass]
    public class ExerimentsOnDatabase
    {


        [TestMethod]
        public void TestConnection()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Assert.IsNotNull(db);
                Assert.IsTrue(db.Meetups.Count() > 0);
            }
        }
    }
}
