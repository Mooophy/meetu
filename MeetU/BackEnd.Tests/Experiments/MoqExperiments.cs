using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MeetU.Models;

namespace BackEnd.Tests.Experiments
{
    public interface ISomeContext
    {
        DbSet<Join> Joins { get; set; }
    }

    public class SomeContext : ISomeContext
    {
        public DbSet<Join> Joins { get; set; }
    }

    public class FooController
    {
        private ISomeContext db;
        public FooController(ISomeContext context)
        {
            db = context;
        }

        public IQueryable<Join> GetMeetups()
        {
            return db.Joins;
        }
    }

    [TestClass]
    public class MoqExperiments
    {
        [TestMethod]
        public void TestMethod()
        {
            var data = new List<Join>{
                new Join {
                    MeetupId = 1,
                    UserId = "some-user-id",
                    UserName = "some-user-name",
                    At = new DateTime()
                },
                new Join {
                    MeetupId = 2,
                    UserId = "some-user-id2",
                    UserName = "some-user-name2",
                    At = new DateTime()
                }
            }.AsQueryable();

            var dbSetMock = new Mock<IDbSet<Meetup>>();
        }
    }
}
