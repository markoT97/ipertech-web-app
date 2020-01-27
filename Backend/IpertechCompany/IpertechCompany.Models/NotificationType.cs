using System;
using IpertechCompany.Models.Validation;

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

        public NotificationType(Guid notificationTypeId, string name, int imageWidth, int imageHeight)
        {
            NotificationTypeId = notificationTypeId;
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
            if (!(!NotificationTypeId.Equals(null) && Name != null && ImageWidth != 0 && ImageHeight != 0))
            {
                return false;
            }

            return true;
        }
    }
}
