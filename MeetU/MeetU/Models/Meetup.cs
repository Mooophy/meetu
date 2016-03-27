using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace MeetU.Models
{
    public class Meetup
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "200 characters at most!")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime When { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [Required]
        public string Where { get; set; }

        public string Sponsor { get; set; }
        [JsonIgnore]
        [ForeignKey("Sponsor")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public bool IsCancelled { get; set; }

        public DateTime? CancelledAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}