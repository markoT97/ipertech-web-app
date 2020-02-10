using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IServices
{
    public interface INotificationService
    {
        IEnumerable<Notification> GetByNotificationTypeId(Guid notificationTypeId);
        Notification CreateNotification(Notification notification);
        void UpdateNotification(Notification notification);
        bool DeleteNotification(Guid notificationId, Guid notificationTypeId);
    }
}
