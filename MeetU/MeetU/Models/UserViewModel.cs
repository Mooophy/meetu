using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetU.Models
{
    public class UserViewModel
    {
        //
        //  Front end uses this property to tell if private info has been hidden
        //
        public bool IsPrivateInfoHidden { get; set; }
        public UserViewModel(bool isPrivateInfoHidden)
        {
            IsPrivateInfoHidden = isPrivateInfoHidden;
        }
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

        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public int LoginCount { get; set; }
    }
}