using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.Services
{
    public class NotificationTypeService : INotificationTypeService
    {
        private readonly INotificationTypeRepository _notificationTypeRepository;

        public NotificationTypeService(INotificationTypeRepository notificationTypeRepository)
        {
            _notificationTypeRepository = notificationTypeRepository;
        }

        public NotificationType CreateNotificationType(NotificationType notificationType)
        {
            if (!(notificationType != null))
            {
                throw new ArgumentNullException("notificationType", "Parameter is null.");
            }

            if (!notificationType.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            return _notificationTypeRepository.Insert(notificationType);
        }

        public bool DeleteNotificationType(Guid notificationTypeId)
        {
            return _notificationTypeRepository.Delete(notificationTypeId);
        }

        public IEnumerable<NotificationType> GetAllNotificationTypes()
        {
            return _notificationTypeRepository.GetAll();
        }

        public void UpdateNotificationType(NotificationType notificationType)
        {
            if (!(notificationType != null))
            {
                throw new ArgumentNullException("notificationType", "Parameter is null.");
            }

            if (!notificationType.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }
            _notificationTypeRepository.Update(notificationType);
        }
    }
}
