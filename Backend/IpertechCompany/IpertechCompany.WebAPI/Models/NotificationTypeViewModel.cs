using System;
using System.ComponentModel.DataAnnotations;

namespace IpertechCompany.WebAPI.Models
{
    public class NotificationTypeViewModel
    {
        [Required]
        public Guid NotificationTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int ImageWidth { get; set; }
        [Required]
        public int ImageHeight { get; set; }
    }
}
