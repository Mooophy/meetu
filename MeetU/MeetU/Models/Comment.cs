using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;


namespace MeetU.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "200 characters at most!")]
        public string Content { get; set; }

        public string By { get; set; }
        [JsonIgnore]
        [ForeignKey("By")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime At { get; set; }
    }
}