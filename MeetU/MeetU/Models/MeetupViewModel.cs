using System.Linq;

namespace MeetU.Models
{
    //
    //  Deprecated
    //
    public class MeetupViewModel
    {
        public Meetup Meetup { get; set; }
        public string SponsorUserName { get; set; }
        public string SponsorNickName { get; set; }
        public IQueryable<Join> Joins { get; set; }
    }
}