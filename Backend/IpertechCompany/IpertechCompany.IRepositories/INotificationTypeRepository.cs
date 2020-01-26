﻿using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IRepositories
{
    public interface INotificationTypeRepository
    {
        NotificationType Insert(NotificationType notificationType);
        void Update(NotificationType notificationType);
        bool Delete(Guid notificationTypeId);
    }
}
