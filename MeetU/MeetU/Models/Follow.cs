using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace MeetU.Models
{
    public class Follow
    {
        [Key, Column(Order = 0)]
        [Required]
        [ForeignKey("Followed")]
        public string FollowedUserId { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser Followed { get; set; }

        [Key, Column(Order = 1)]
        [Required]
        [ForeignKey("Following")]
        public string FollowingUserId { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser Following { get; set; }

        [Required]
        public DateTime At { get; set; }
    }
}