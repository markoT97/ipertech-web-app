using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IServices
{
    public interface INotificationService
    {
        IEnumerable<Notification> GetAllNotifications();
        IEnumerable<Notification> GetAllNotifications(int numberOfNewestRows);
        IEnumerable<Notification> GetByNotificationTypeId(Guid notificationTypeId, int? numberOfNewestRows = null);
        IEnumerable<Notification> GetByNotificationTypeName(string notificationTypeName, int? numberOfNewestRows = null);
        Notification CreateNotification(Notification notification);
        void UpdateNotification(Notification notification);
        bool DeleteNotification(Guid notificationId, Guid notificationTypeId);
    }
}
