using System;
using System.ComponentModel.DataAnnotations;

namespace IpertechCompany.WebAPI.Models
{
    public class InternetRouterViewModel
    {
        [Required]
        public Guid InternetRouterId { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImageLocation { get; set; }
    }
}
