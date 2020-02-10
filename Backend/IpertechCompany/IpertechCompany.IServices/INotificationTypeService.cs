using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IServices
{
    public interface INotificationTypeService
    {
        IEnumerable<NotificationType> GetAllNotificationTypes();
        NotificationType CreateNotificationType(NotificationType notificationType);
        void UpdateNotificationType(NotificationType notificationType);
        bool DeleteNotificationType(Guid notificationTypeId);
    }
}
