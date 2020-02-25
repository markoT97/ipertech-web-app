using IpertechCompany.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace IpertechCompany.WebAPI.Models
{
    public class InternetPacketViewModel
    {
        [Required]
        public Guid InternetPacketId { get; set; }
        [Required]
        public InternetRouter InternetRouter { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Speed { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
