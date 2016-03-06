using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MeetU.Models;
using MeetU.API;

//
//  This experiment shows how to unit test an API, using Moq.
//
namespace BackEnd.Tests.Experiments
{
    [TestClass]
    public class MoqExperiments
    {
        [TestMethod]
        public void MoqExperiment()
        {
            //
            //  Arrange
            //
            
            //  fake data
            var data = new List<Join>{
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

            //  mock DbSet
            //  may be abstracted out later
            var dbSetMock = new Mock<IDbSet<Join>>();
            dbSetMock.Setup(m => m.Provider).Returns(data.Provider);
            dbSetMock.Setup(m => m.Expression).Returns(data.Expression);
            dbSetMock.Setup(m => m.ElementType).Returns(data.ElementType);
            dbSetMock.Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            //  mock context
            var contextMock = new Mock<MuDbContext>();
            contextMock.Setup(x => x.Joins).Returns(dbSetMock.Object);

            // The contoller to test
            var c = new JoinsController(contextMock.Object);

            //
            //  Tests
            //  

            // GET: api/Joins
            {
                var js = c.GetJoins();
                Assert.AreEqual(2, js.Count());
                Assert.IsNotNull(js.FirstOrDefault(
                    j => 
                        j.UserId == "some-user-id2" && j.MeetupId == 42
                    ));
            }
        }
    }
}
