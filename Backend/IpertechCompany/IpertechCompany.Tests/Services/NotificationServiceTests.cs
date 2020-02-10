using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using IpertechCompany.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IpertechCompany.Tests.Services
{
    [TestFixture]
    public class NotificationServiceTests
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationService _notificationService;

        public NotificationServiceTests()
        {
            _notificationRepository = Substitute.For<INotificationRepository>();
            _notificationService = new NotificationService(_notificationRepository);
        }

        [Test]
        public void CreateNotification_NullObject_ExpectsException()
        {
            _notificationRepository.Insert(null).Throws<NullReferenceException>();

            Assert.Throws<ArgumentNullException>(() => _notificationService.CreateNotification(null));
            _notificationRepository.DidNotReceive().Insert(null);
        }

        [Test]
        public void CreateNotification_WithoutRequiredFields_ExpectsException()
        {
            var notification = new Notification(Guid.NewGuid(), new NotificationType());
            _notificationRepository.Insert(notification).Returns(notification);

            Assert.Throws<ArgumentException>(() => _notificationService.CreateNotification(notification));
            _notificationRepository.DidNotReceive().Insert(notification);
        }

        [Test]
        public void CreateNotification_WithRequiredFields_ReturnsNotification()
        {
            var notification = new Notification(Guid.Parse("F6AFCC3E-DD34-421B-8573-23695441F910"), new NotificationType(Guid.Parse("B953E5F6-2DE5-4B9C-B2C4-17E62DDAE850")), "Naslov", "Tekst", "www/notifications/prom-1.png"); ;
            _notificationRepository.Insert(notification).Returns(notification);

            var returnedNotification = _notificationService.CreateNotification(notification);
            _notificationRepository.Received(1).Insert(notification);
            Assert.AreEqual(notification, returnedNotification);
        }

        [Test]
        public void UpdateNotification_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _notificationService.UpdateNotification(null));
            _notificationRepository.DidNotReceive().Update(null);
        }

        [Test]
        public void UpdateNotification_WithoutRequiredFields_ExpectsException()
        {
            var notification = new Notification();

            Assert.Throws<ArgumentException>(() => _notificationService.UpdateNotification(notification));
            _notificationRepository.DidNotReceive().Update(notification);
        }

        [Test]
        public void UpdateNotification_WithRequiredFields_ReturnsNothing()
        {
            var notification = new Notification(Guid.Parse("F6AFCC3E-DD34-421B-8573-23695441F910"), new NotificationType(Guid.Parse("B953E5F6-2DE5-4B9C-B2C4-17E62DDAE850")), "Naslov", "Tekst", "www/notifications/prom-1.png");
            _notificationService.UpdateNotification(notification);
            _notificationRepository.Received(1).Update(notification);
        }

        [Test]
        public void DeleteNotification_WhichNotExists_ReturnsTrue()
        {
            var notificationId = Guid.NewGuid();
            var notificationTypeId = Guid.NewGuid();

            _notificationRepository.Delete(notificationId, notificationTypeId).Returns(false);
            Assert.False(_notificationService.DeleteNotification(notificationId, notificationTypeId));
        }

        [Test]
        public void DeleteNotification_WhichExists_ReturnsTrue()
        {
            var notificationId = Guid.Parse("F6AFCC3E-DD34-421B-8573-23695441F910");
            var notificationTypeId = Guid.Parse("B953E5F6-2DE5-4B9C-B2C4-17E62DDAE850");

            _notificationRepository.Delete(notificationId, notificationTypeId).Returns(true);
            Assert.True(_notificationService.DeleteNotification(notificationId, notificationTypeId));
        }

        [Test]
        public void GetNotificationByNotificationTypeId_WithoutData_ReturnsNullObject()
        {
            var notificationTypeId = Guid.Parse("B953E5F6-2DE5-4B9C-B2C4-17E62DDAE850");
            _notificationRepository.Get(notificationTypeId)
                .Returns(new List<Notification>());

            var returnedNotification = _notificationService.GetByNotificationTypeId(notificationTypeId);
            Assert.AreEqual(0, returnedNotification.Count());
        }

        [Test]
        public void GetNotificationByNotificationTypeId_WithData_ReturnsNotification()
        {
            var notifications = new List<Notification>()
            {
                new Notification(Guid.Parse("F6AFCC3E-DD34-421B-8573-23695441F910"), new NotificationType(Guid.Parse("B953E5F6-2DE5-4B9C-B2C4-17E62DDAE850")), "Naslov", "Tekst", "www/notifications/prom-1.png"),
                new Notification(Guid.Parse("B10C5F84-73FB-4211-8959-5A9E35403207"), new NotificationType(Guid.Parse("B953E5F6-2DE5-4B9C-B2C4-17E62DDAE850")), "Naslov", "Tekst", "www/notifications/prom-2.png"),
                new Notification(Guid.Parse("D6D21485-EC75-47D7-9A65-7B3789FA53FD"), new NotificationType(Guid.Parse("B953E5F6-2DE5-4B9C-B2C4-17E62DDAE850")), "Naslov", "Tekst", "www/notifications/prom-3.png"),
                new Notification(Guid.Parse("480C536D-20B1-4F64-B6C3-B3F122ED4639"), new NotificationType(Guid.Parse("9FBA9021-1A99-47F6-ABC7-C132C3AC7A0E")), "Radovi na rekonstrukciji infrastrukture", "U ulici Stevana Momcilovica, Bulevaru Patrijarha Pavla, Bulevar i Cara Lazara 06.01.2020. ...", "www/notifications/ob-1.png"),
            };
            var notificationTypeId = Guid.Parse("B953E5F6-2DE5-4B9C-B2C4-17E62DDAE850");
            _notificationRepository.Get(notificationTypeId)
                .Returns(notifications.Where(ir => ir.NotificationType.NotificationTypeId == notificationTypeId));

            var returnedNotifications = _notificationService.GetByNotificationTypeId(notificationTypeId);
            Assert.AreEqual(notifications.Where(ir => ir.NotificationType.NotificationTypeId == notificationTypeId).Count(), returnedNotifications.Count());
        }
    }
}
