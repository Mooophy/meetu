using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetU.Models;

namespace BackEnd.Tests.Experiments
{
    public interface ISomeContext
    {
        DbSet<Meetup> Meetups { get; set; }
    }

    [TestClass]
    public class MoqExperiments
    {
        [TestMethod]
        public void TestMethod()
        {

        }
    }
}
