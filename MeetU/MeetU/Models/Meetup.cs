using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetU.Models
{
    public class Meetup
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "40 characters at most!")]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime When { get; set; }

        [Required]
        public string Where { get; set; }

        public string Sponsor { get; set; }
        [ForeignKey("Sponsor")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}