using IpertechCompany.Models.Validation;
using System;

namespace IpertechCompany.Models
{
    public class NotificationType : IValidation
    {
        public Guid NotificationTypeId { get; set; }
        public string Name { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }

        public NotificationType()
        {

        }

        public NotificationType(Guid notificationTypeId, string name = null, int imageWidth = 0, int imageHeight = 0)
        {
            NotificationTypeId = notificationTypeId.Equals(Guid.Empty) ? Guid.NewGuid() : notificationTypeId;
            Name = name;
            ImageWidth = imageWidth;
            ImageHeight = imageHeight;
        }

        public override string ToString()
        {
            return NotificationTypeId + ", " + Name + ", " + ImageWidth + ", " + ImageHeight;
        }
        public bool IsValid()
        {
            if (!(!NotificationTypeId.Equals(Guid.Empty) && Name != null && ImageWidth != 0 && ImageHeight != 0))
            {
                return false;
            }

            return true;
        }
    }
}
