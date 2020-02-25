using IpertechCompany.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace IpertechCompany.WebAPI.Models
{
    public class BillViewModel
    {
        [Required]
        public Guid BillId { get; set; }
        [Required]
        public UserContract UserContract { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string CallNum { get; set; }
        [Required]
        public string AccOfRecipient { get; set; }
        [Required]
        public bool IsPaid { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Currency { get; set; }
    }
}
