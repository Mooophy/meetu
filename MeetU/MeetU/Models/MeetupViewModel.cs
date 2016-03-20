﻿using System.Linq;

namespace MeetU.Models
{
    public class MeetupViewModel
    {
        public Meetup Meetup { get; set; }
        public string SponsorUserName { get; set; }
        public IQueryable<Join> Joins { get; set; }
    }
}