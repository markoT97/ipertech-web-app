using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models.Validation;

namespace IpertechCompany.Models
{
    public class Notification : IValidation
    {
        public Guid NotificationId { get; set; }
        public Guid NotificationTypeId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageLocation { get; set; }

        public Notification()
        {
            NotificationId = Guid.NewGuid();
        }

        public Notification(Guid notificationId, Guid notificationTypeId, string title, string content, string imageLocation)
        {
            NotificationId = notificationId;
            NotificationTypeId = notificationTypeId;
            Title = title;
            Content = content;
            ImageLocation = imageLocation;
        }

        public override string ToString()
        {
            return NotificationId + ", " + NotificationTypeId + ", " + Title + ", " + Content + ", " + ImageLocation;
        }

        public bool IsValid()
        {
            if (!(!NotificationId.Equals(null) && !NotificationTypeId.Equals(null) && Title != null &&
                  ImageLocation != null))
            {
                return false;
            }

            return true;
        }
    }
}
