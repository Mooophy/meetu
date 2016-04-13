using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MeetU.Models;

namespace MeetU.ViewModels
{
    public class MeetupView
    {
        public Meetup Meetup { get; set; }
        public string SponsorUserName { get; set; }
        public string SponsorNickName { get; set; }
        public IQueryable<JoinView> JoinViews { get; set; }
    }
}