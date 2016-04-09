using System.ComponentModel.DataAnnotations;

namespace MeetU.Models
{
    public class ProfileNotInUsers
    {
        [Key]
        public string UserId { get; set; }
    }
}