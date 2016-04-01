using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetU.Models
{
    public class PublicUserViewModel
    {
        //
        // Of table AspNetUsers
        //

        // Read only
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int Number { get; set; }

        //
        // Of table Profiles
        //

        // Read and Update
        public string NickName { get; set; }
        public string Picture { get; set; }
        public string Gender { get; set; }
        public string Brief { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class PrivateUserViewModel: PublicUserViewModel
    {
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public int LoginCount { get; set; }
    }
}