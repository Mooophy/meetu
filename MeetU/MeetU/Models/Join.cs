using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace MeetU.Models
{
    public class Join
    {
        [Key, Column(Order = 0)]
        [Required]
        [ForeignKey("Meetup")]
        public int MeetupId { get; set; }

        [JsonIgnore]
        public virtual Meetup Meetup { get; set; }

        [Key, Column(Order = 1)]
        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        public string UserName { get; set; }

        [JsonIgnore]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public DateTime At { get; set; }
    }
}