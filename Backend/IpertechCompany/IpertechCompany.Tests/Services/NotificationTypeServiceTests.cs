using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using IpertechCompany.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IpertechCompany.Tests.Services
{
    [TestFixture]
    public class NotificationTypeServiceTests
    {
        private readonly INotificationTypeRepository _notificationTypeRepository;
        private readonly INotificationTypeService _notificationTypeService;

        public NotificationTypeServiceTests()
        {
            _notificationTypeRepository = Substitute.For<INotificationTypeRepository>();
            _notificationTypeService = new NotificationTypeService(_notificationTypeRepository);
        }

        [Test]
        public void CreateNotificationType_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _notificationTypeService.CreateNotificationType(null));
            _notificationTypeRepository.DidNotReceive().Insert(null);
        }

        [Test]
        public void CreateNotificationType_WithoutRequitedFields_ExpectsException()
        {
            var notificationType = new NotificationType();

            Assert.Throws<ArgumentException>(() => _notificationTypeService.CreateNotificationType(notificationType));
            _notificationTypeRepository.DidNotReceive().Insert(notificationType);
        }

        [Test]
        public void CreateNotificationType_WithRequiredFields_ReturnsNotificationType()
        {
            var notificationType = new NotificationType(Guid.Parse("B953E5F6-2DE5-4B9C-B2C4-17E62DDAE850"), "Promocije", 800, 200);
            _notificationTypeRepository.Insert(notificationType).Returns(notificationType);

            var returnedNotificationType = _notificationTypeService.CreateNotificationType(notificationType);
            _notificationTypeRepository.Received(1).Insert(notificationType);
            Assert.AreEqual(notificationType, returnedNotificationType);
        }

        [Test]
        public void UpdateNotificationType_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _notificationTypeService.UpdateNotificationType(null));
            _notificationTypeRepository.DidNotReceive().Update(null);
        }

        [Test]
        public void UpdateNotificationType_WithoutRequiredFields_ExpectsException()
        {
            var notificationType = new NotificationType();

            Assert.Throws<ArgumentException>(() => _notificationTypeService.UpdateNotificationType(notificationType));
            _notificationTypeRepository.DidNotReceive().Update(notificationType);
        }

        [Test]
        public void UpdateNotificationType_WithRequiredFields_ReturnsNothing()
        {
            var notificationType = new NotificationType(Guid.Parse("B953E5F6-2DE5-4B9C-B2C4-17E62DDAE850"), "Promocije", 800, 200);
            _notificationTypeService.UpdateNotificationType(notificationType);
            _notificationTypeRepository.Received(1).Update(notificationType);
        }

        [Test]
        public void GetAllNotificationTypes_WithoutData_ReturnsEmptyList()
        {
            _notificationTypeRepository.GetAll().Returns(new List<NotificationType>());
            Assert.AreEqual(0, _notificationTypeService.GetAllNotificationTypes().Count());
        }

        [Test]
        public void GetAllNotificationTypes_WithData_ReturnsPopulatedList()
        {
            var notificationTypes = new List<NotificationType>()
            {
                new NotificationType(Guid.Parse("B953E5F6-2DE5-4B9C-B2C4-17E62DDAE850"), "Promocije", 800, 200),
                new NotificationType(Guid.Parse("9FBA9021-1A99-47F6-ABC7-C132C3AC7A0E"), "Obaveštenja", 250, 200)
            };
            _notificationTypeRepository.GetAll().Returns(notificationTypes);

            Assert.AreEqual(notificationTypes.Count, _notificationTypeService.GetAllNotificationTypes().Count());
        }
    }
}
