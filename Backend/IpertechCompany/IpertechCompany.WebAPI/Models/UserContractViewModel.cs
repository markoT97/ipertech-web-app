using IpertechCompany.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace IpertechCompany.WebAPI.Models
{
    public class UserContractViewModel
    {
        [Required]
        public Guid UserContractId { get; set; }
        [Required]
        public PacketCombination PacketCombination { get; set; }
        [Required]
        public int ContractDurationMonths { get; set; }
    }
}
