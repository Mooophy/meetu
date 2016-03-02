//
//  The unique database context used in this project
//  @Yue
//
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MeetU.Models
{
    public class DbContext : IdentityDbContext<ApplicationUser>
    {
        public DbContext()
            : base("MeetUDB", throwIfV1Schema: false)
        {
        }

        public static DbContext Create()
        {
            return new DbContext();
        }

        public DbSet<Meetup> Meetups { get; set; }
        public DbSet<Join> Joins { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}