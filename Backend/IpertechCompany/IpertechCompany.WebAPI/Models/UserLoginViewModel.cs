using System.ComponentModel.DataAnnotations;

namespace IpertechCompany.WebAPI.Models
{
    public class UserLoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
