using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace IpertechCompany.WebAPI.Models
{
    public class NotificationViewModel
    {
        public Guid NotificationId { get; set; }
        [Required]
        public Guid NotificationTypeId { get; set; }
        public string NotificationTypeName { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string ImageLocation { get; set; }
        public IFormFile Image { get; set; }
    }
}
