using IpertechCompany.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace IpertechCompany.WebAPI.Models
{
    public class PollOptionViewModel
    {
        [Required]
        public Guid PollOptionId { get; set; }
        [Required]
        public Poll Poll { get; set; }
        [Required]
        public string AnswerText { get; set; }
    }
}
