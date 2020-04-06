using System;
using System.ComponentModel.DataAnnotations;

namespace IpertechCompany.WebAPI.Models
{
    public class UserPasswordViewModel
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
