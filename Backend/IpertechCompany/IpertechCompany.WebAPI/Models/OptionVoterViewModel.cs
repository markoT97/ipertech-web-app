using System;
using System.ComponentModel.DataAnnotations;

namespace IpertechCompany.WebAPI.Models
{
    public class OptionVoterViewModel
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid PollId { get; set; }
        [Required]
        public Guid PollOptionId { get; set; }
    }
}
