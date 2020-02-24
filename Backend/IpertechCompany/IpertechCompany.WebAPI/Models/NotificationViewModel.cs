using IpertechCompany.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace IpertechCompany.WebAPI.Models
{
    public class NotificationViewModel
    {
        public Guid NotificationId { get; set; }
        [Required]
        public NotificationType NotificationType { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string ImageLocation { get; set; }
    }
}
