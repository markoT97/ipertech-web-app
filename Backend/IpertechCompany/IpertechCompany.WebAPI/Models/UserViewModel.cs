using IpertechCompany.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IpertechCompany.WebAPI.Models
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }
        [Required]
        public UserContract UserContract { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Gender { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        public string ImageLocation { get; set; }
        public List<BillViewModel> Bills { get; set; }
    }
}
