using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IRepositories
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetAll();
        IEnumerable<Notification> GetAll(int numberOfNewestRows);
        IEnumerable<Notification> Get(Guid notificationTypeId);
        Notification Insert(Notification notification);
        void Update(Notification notification);
        bool Delete(Guid notificationId, Guid notificationTypeId);
    }
}
