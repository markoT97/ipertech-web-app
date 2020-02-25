using System;
using System.ComponentModel.DataAnnotations;

namespace IpertechCompany.WebAPI.Models
{
    public class PollViewModel
    {
        [Required]
        public Guid PollId { get; set; }
        [Required]
        public string Question { get; set; }
        public int NumberOfVoters { get; set; }
    }
}
