using IpertechCompany.Models.Validation;
using System;

namespace IpertechCompany.Models
{
    public class Notification : IValidation
    {
        public Guid NotificationId { get; set; }
        public NotificationType NotificationType { get; set; }
        public Guid NotificationTypeId { get; set; }
        public string NotificationTypeName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageLocation { get; set; }

        public Notification()
        {
            NotificationType = new NotificationType();
        }

        public Notification(Guid notificationId, NotificationType notificationType = null, string title = null, string content = null, string imageLocation = null)
        {
            NotificationId = notificationId.Equals(Guid.Empty) ? Guid.NewGuid() : notificationId;
            NotificationType = notificationType;
            Title = title;
            Content = content;
            CreatedAt = DateTime.UtcNow;
            ImageLocation = imageLocation;
        }

        public override string ToString()
        {
            return NotificationId + ", " + NotificationType + ", " + Title + ", " + Content + ", " + ImageLocation;
        }

        public bool IsValid()
        {
            if (!(!NotificationId.Equals(Guid.Empty) && !NotificationTypeId.Equals(Guid.Empty) && Title != null
                  ))
            {
                return false;
            }

            return true;
        }
    }
}
