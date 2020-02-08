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
    public class NotificationTypeRepositoryTests
    {
        private readonly IDbContext _dbContext;
        private readonly INotificationTypeRepository _notificationTypeRepository;

        public NotificationTypeRepositoryTests()
        {
            _dbContext = new DbContext("Data Source=DESKTOP-883AG4N\\SQLEXPRESS;Initial Catalog=IpertechCompany;Integrated Security=True");
            _notificationTypeRepository = new NotificationTypeRepository(_dbContext);
        }

        [Test]
        public void GetAll_WithData_ReturnsListOfOneNotificationType()
        {
            Assert.AreEqual(1, _notificationTypeRepository.GetAll().Count());
        }

        [Test]
        public void Insert_WithRequiredFields_ReturnsNotificationType()
        {
            var notificationType = new NotificationType(Guid.NewGuid(), "Insert Name", 100, 100);

            _notificationTypeRepository.Insert(notificationType);
            Assert.AreEqual(2, _notificationTypeRepository.GetAll().Count());
        }

        [Test]
        public void Insert_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _notificationTypeRepository.Insert(null));
        }

        [Test]
        public void Update_WithRequiredFields_ReturnsNotificationType()
        {
            var notificationType = new NotificationType(Guid.Parse("B953E5F6-2DE5-4B9C-B2C4-17E62DDAE850"), "Update Name", 100, 100);

            _notificationTypeRepository.Update(notificationType);

            var updatedInternetPacket = _notificationTypeRepository
                .GetAll().Single(nt => nt.NotificationTypeId == notificationType.NotificationTypeId);

            Assert.AreEqual(notificationType.Name, updatedInternetPacket.Name);
        }

        [Test]
        public void Update_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _notificationTypeRepository.Update(null));
        }

        [Test]
        public void Delete_WhichExists_ReturnsTrue()
        {
            Assert.True(_notificationTypeRepository.Delete(Guid.Parse("9FBA9021-1A99-47F6-ABC7-C132C3AC7A0E")));
        }

        [Test]
        public void Delete_WhichNotExists_ReturnsFalse()
        {
            Assert.False(_notificationTypeRepository.Delete(Guid.NewGuid()));
        }
    }
}
