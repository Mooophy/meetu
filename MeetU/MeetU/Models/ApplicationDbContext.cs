//
//  The unique database context used in this project
//  @Yue
//
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MeetU.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("MeetUDB", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Meetup> Meetups { get; set; }
        public DbSet<Join> Joins { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}