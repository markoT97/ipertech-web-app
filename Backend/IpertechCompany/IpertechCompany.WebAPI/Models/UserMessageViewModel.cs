using IpertechCompany.Models;
using System.ComponentModel.DataAnnotations;

namespace IpertechCompany.WebAPI.Models
{
    public class UserMessageViewModel
    {
        [Required]
        public User User { get; set; }
        [Required]
        public Message Message { get; set; }
    }
}
