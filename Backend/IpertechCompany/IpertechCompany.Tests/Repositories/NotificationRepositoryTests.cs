using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IpertechCompany.DbConnection;
using IpertechCompany.DbRepositories;
using IpertechCompany.IRepositories;
using IpertechCompany.Models;
using NUnit.Framework;

namespace IpertechCompany.Tests.Repositories
{
    [TestFixture]
    public class NotificationRepositoryTests
    {
        private readonly IDbContext _dbContext;
        private readonly INotificationRepository _notificationRepository;

        public NotificationRepositoryTests()
        {
            _dbContext = new DbContext("Data Source=DESKTOP-883AG4N\\SQLEXPRESS;Initial Catalog=IpertechCompany;Integrated Security=True");
            _notificationRepository = new NotificationRepository(_dbContext);
        }

        [Test]
        public void GetByNotificationTypeId_WithExistingNotificationType_ReturnsPopulatedList()
        {
            Assert.AreEqual(5, _notificationRepository.Get(Guid.Parse("9FBA9021-1A99-47F6-ABC7-C132C3AC7A0E")).Count());
        }
        
        [Test]
        public void GetByNotificationTypeId_WithoutExistingNotificationType_ReturnsEmptyList()
        {
            Assert.AreEqual(0, _notificationRepository.Get(Guid.NewGuid()).Count());
        }

        [Test]
        public void Insert_WithRequiredFields_ReturnsNotification()
        {
            var notification = new Notification(Guid.NewGuid(), Guid.Parse("9FBA9021-1A99-47F6-ABC7-C132C3AC7A0E"), "Insert Title", "Insert Content", "Insert Location");

            _notificationRepository.Insert(notification);
            Assert.AreEqual(notification.Title, _notificationRepository.Get(notification.NotificationTypeId).First(n => n.NotificationId == notification.NotificationId).Title);
        }

        [Test]
        public void Insert_NullObject_ThrowsException()
        {
            Assert.Throws<NullReferenceException>(() => _notificationRepository.Insert(null));
        }

        [Test]
        public void Update_WithRequiredFields_ReturnsNotification()
        {
            var notification = new Notification(Guid.Parse("F6AFCC3E-DD34-421B-8573-23695441F910"), Guid.Parse("B953E5F6-2DE5-4B9C-B2C4-17E62DDAE850"), "Update Title", "Update Content", "Update Location");

            _notificationRepository.Update(notification);
            var updatedNotification = _notificationRepository.Get(notification.NotificationTypeId)
                .First(n => n.NotificationId == notification.NotificationId);

            Assert.AreEqual(notification.Title, updatedNotification.Title);
        }

        [Test]
        public void Update_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _notificationRepository.Update(null));
        }

        [Test]
        public void Delete_WhichExists_ReturnsTrue()
        {
            Assert.True(_notificationRepository.Delete(Guid.Parse("8C5FEA09-17C4-4C5C-A50C-A76190D683BB"), Guid.Parse("B953E5F6-2DE5-4B9C-B2C4-17E62DDAE850")));
        }

        [Test]
        public void Delete_WhichNotExists_ReturnsFalse()
        {
            Assert.False(_notificationRepository.Delete(Guid.NewGuid(), Guid.NewGuid()));
        }
    }
}
