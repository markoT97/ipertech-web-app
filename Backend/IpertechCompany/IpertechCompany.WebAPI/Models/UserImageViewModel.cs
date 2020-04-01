using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace IpertechCompany.WebAPI.Models
{
    public class UserImageViewModel
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
