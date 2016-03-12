using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace MeetU.Models
{
    public class Profile
    {
        [JsonIgnore]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Key]
        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public string NickName { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Pricture { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? LoginCount { get; set; }
    }
}