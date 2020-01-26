using IpertechCompany.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IpertechCompany.IRepositories
{
    interface INotificationRepository
    {
        IEnumerable<Notification> Get(Guid notificationTypeId);
        Notification Insert(Notification notification);
        void Update(Notification notification);
        bool Delete(Guid notificationId);
    }
}
