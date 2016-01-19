using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetU.Models.Domain
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20,ErrorMessage ="20 characters at most!")]
        public string Name { get; set; }
        [MaxLength(100, ErrorMessage = "100 characters at most!")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Created By")]
        public string By { get; set; }
        [ForeignKey("By")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}