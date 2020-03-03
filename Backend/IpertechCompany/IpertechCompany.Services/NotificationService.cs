using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public Notification CreateNotification(Notification notification)
        {
            if (!(notification != null))
            {
                throw new ArgumentNullException("notification", "Parameter is null.");
            }

            if (!notification.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            return _notificationRepository.Insert(notification);
        }

        public bool DeleteNotification(Guid notificationId, Guid notificationTypeId)
        {
            return _notificationRepository.Delete(notificationId, notificationTypeId);
        }

        public IEnumerable<Notification> GetAllNotifications()
        {
            return _notificationRepository.GetAll();
        }

        public IEnumerable<Notification> GetAllNotifications(int numberOfNewestRows)
        {
            return _notificationRepository.GetAll(numberOfNewestRows);
        }

        public IEnumerable<Notification> GetByNotificationTypeId(Guid notificationTypeId)
        {
            return _notificationRepository.Get(notificationTypeId);
        }

        public void UpdateNotification(Notification notification)
        {
            if (!(notification != null))
            {
                throw new ArgumentNullException("notification", "Parameter is null.");
            }

            if (!notification.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            _notificationRepository.Update(notification);
        }
    }
}
