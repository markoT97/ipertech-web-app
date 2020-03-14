using System;
using System.Collections.Generic;
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
        public List<PollOptionViewModel> Options { get; set; }
    }
}
