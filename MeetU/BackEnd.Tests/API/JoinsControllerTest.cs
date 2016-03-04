using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MeetU.Models;
using MeetU.API;

namespace BackEnd.Tests.API
{
    [TestClass]
    public class JoinsControllerTest
    {
        public JoinsControllerTest()
        {
            Data = MakeData();

            DbSetMock = new Mock<IDbSet<Join>>();
            DbSetMock.Setup(m => m.Provider).Returns(Data.Provider);
            DbSetMock.Setup(m => m.Expression).Returns(Data.Expression);
            DbSetMock.Setup(m => m.ElementType).Returns(Data.ElementType);
            DbSetMock.Setup(m => m.GetEnumerator()).Returns(Data.GetEnumerator());

            ContextMock = new Mock<MuDbContext>();
            ContextMock.Setup(x => x.Joins).Returns(DbSetMock.Object);
            //ContextMock.Setup(c => c.Sa).Returns(() => Task.Run(() => { })).Verifiable();

            Controller = new MeetU.API.JoinsController(ContextMock.Object);


        }

        public IQueryable<Join> MakeData()
        {
            return  new List<Join>{
                new Join{
                    MeetupId = 42,
                    UserId = "some-user-id2",
                    UserName = "some-user-name2",
                    At = new DateTime()
                },
                new Join {
                    MeetupId = 99,
                    UserId = "some-user-id",
                    UserName = "some-user-name",
                    At = new DateTime()
                }
            }.AsQueryable();
        }

        public IQueryable<Join> Data { get; set; }
        public Mock<IDbSet<Join>> DbSetMock { get; set; }
        public Mock<MuDbContext> ContextMock { get; set; }
        public JoinsController Controller { get; set; }

        //
        //  GET: api/Joins
        //
        [TestMethod]
        public void TestGetJoins()
        {
            Data = MakeData();

            var js = Controller.GetJoins();
            Assert.AreEqual(2, js.Count());
            Assert.IsNotNull(js.FirstOrDefault(
                j =>
                    j.UserId == "some-user-id2" && j.MeetupId == 42
                ));
        }
    }
}
