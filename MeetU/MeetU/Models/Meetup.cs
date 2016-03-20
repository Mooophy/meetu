﻿using System;
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
        [MaxLength(40, ErrorMessage = "40 characters at most!")]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan When { get; set; }

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
    }
}