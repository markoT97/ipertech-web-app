using System;
using System.ComponentModel.DataAnnotations;

namespace IpertechCompany.WebAPI.Models
{
    public class MessageViewModel
    {
        public Guid MessageId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime? CreatedAt { get; set; }
        [Required]
        public string Category { get; set; }
    }
}
