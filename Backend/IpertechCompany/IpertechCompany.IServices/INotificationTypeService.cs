using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

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
