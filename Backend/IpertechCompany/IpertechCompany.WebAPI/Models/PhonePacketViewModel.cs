using System;
using System.ComponentModel.DataAnnotations;

namespace IpertechCompany.WebAPI.Models
{
    public class PhonePacketViewModel
    {
        [Required]
        public Guid PhonePacketId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int FreeMinutes { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
