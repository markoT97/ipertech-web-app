using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using IpertechCompany.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace IpertechCompany.Tests.Services
{
    [TestFixture]
    public class InternetRouterServiceTests
    {
        private readonly IInternetRouterRepository _internetRouterRepository;
        private readonly IInternetRouterService _internetRouterService;

        public InternetRouterServiceTests()
        {
            _internetRouterRepository = Substitute.For<IInternetRouterRepository>();
            _internetRouterService = new InternetRouterService(_internetRouterRepository);
        }

        [Test]
        public void CreateInternetRouter_NullObject_ExpectsException()
        {
            _internetRouterRepository.Insert(null).Throws<NullReferenceException>();

            Assert.Throws<ArgumentNullException>(() => _internetRouterService.CreateInternetRouter(null));
            _internetRouterRepository.DidNotReceive().Insert(null);
        }

        [Test]
        public void CreateInternetRouter_WithoutRequiredFields_ExpectsException()
        {
            var internetRouter = new InternetRouter(Guid.NewGuid());
            _internetRouterRepository.Insert(internetRouter).Returns(internetRouter);

            Assert.Throws<ArgumentException>(() => _internetRouterService.CreateInternetRouter(internetRouter));
            _internetRouterRepository.DidNotReceive().Insert(internetRouter);
        }

        [Test]
        public void CreateInternetRouter_WithRequiredFields_ReturnsInternetRouter()
        {
            var internetRouter = new InternetRouter(Guid.Parse("59659676-8043-49FC-804D-1621650838C7"),
                "Asus RT-AC66U B1 Dual-Band Gigabit Wi-Fi Router",
                "www/packets/internet-routers/asus-rt-ac66u.png");
            _internetRouterRepository.Insert(internetRouter).Returns(internetRouter);

            var returnedInternetRouter = _internetRouterService.CreateInternetRouter(internetRouter);
            _internetRouterRepository.Received(1).Insert(internetRouter);
            Assert.AreEqual(internetRouter, returnedInternetRouter);
        }

        [Test]
        public void UpdateInternetRouter_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _internetRouterService.UpdateInternetRouter(null));
            _internetRouterRepository.DidNotReceive().Update(null);
        }

        [Test]
        public void UpdateInternetRouter_WithoutRequiredFields_ExpectsException()
        {
            var internetRouter = new InternetRouter();

            Assert.Throws<ArgumentException>(() => _internetRouterService.UpdateInternetRouter(internetRouter));
            _internetRouterRepository.DidNotReceive().Update(internetRouter);
        }

        [Test]
        public void UpdateInternetRouter_WithRequiredFields_ReturnsNothing()
        {
            var internetRouter = new InternetRouter(Guid.Parse("59659676-8043-49FC-804D-1621650838C7"),
                "Asus RT-AC66U B1 Dual-Band Gigabit Wi-Fi Router",
                "www/packets/internet-routers/asus-rt-ac66u.png");
            _internetRouterService.UpdateInternetRouter(internetRouter);
            _internetRouterRepository.Received(1).Update(internetRouter);
        }

        [Test]
        public void DeleteInternetRouter_WhichNotExists_ReturnsTrue()
        {
            var internetRouterId = Guid.NewGuid();

            _internetRouterRepository.Delete(internetRouterId).Returns(false);
            Assert.False(_internetRouterService.DeleteInternetRouter(internetRouterId));
        }

        [Test]
        public void DeleteInternetRouter_WhichExists_ReturnsTrue()
        {
            var internetRouterId = Guid.Parse("4F6327C6-E149-4F86-BD09-426BFF1178B2");

            _internetRouterRepository.Delete(internetRouterId).Returns(true);
            Assert.True(_internetRouterService.DeleteInternetRouter(internetRouterId));
        }

        [Test]
        public void GetInternetRouterByInternetRouterId_WithoutData_ReturnsNullObject()
        {
            var internetRouterId = Guid.Parse("59659676-8043-49FC-804D-1621650838C7");
            _internetRouterRepository.Get(internetRouterId)
                .Returns(new InternetRouter());

            var returnedInternetRouter = _internetRouterService.GetByInternetRouterId(internetRouterId);
            Assert.AreEqual(Guid.Empty, returnedInternetRouter.InternetRouterId);
        }

        [Test]
        public void GetInternetRouterByInternetRouterId_WithData_ReturnsInternetRouter()
        {
            var internetRouters = new List<InternetRouter>()
            {
                new InternetRouter(Guid.Parse("59659676-8043-49FC-804D-1621650838C7"),
                    "Asus RT-AC66U B1 Dual-Band Gigabit Wi-Fi Router",
                    "www/packets/internet-routers/asus-rt-ac66u.png"),
                new InternetRouter(Guid.Parse("FEDF43C3-04F4-4DCF-8959-23511DEF6B45"), "Asus ROG Rapture GT-AC5300",
                    "www/packets/internet-routers/asus-gt-ac5300.png"),
                new InternetRouter(Guid.Parse("32278526-B2EA-44E6-A5C8-49C4AAE6CA2E"),
                    "Jetstream AC3000 Tri-Band Wi-Fi Gaming Router",
                    "www/packets/internet-routers/jetstream-gt-ac3000.png"),
                new InternetRouter(Guid.Parse("62359BE4-1124-4AE5-AC1B-72928E2354E8"), "Asus RT-AX88U",
                    "www/packets/internet-routers/asus-rt-ax88u.png"),
                new InternetRouter(Guid.Parse("66C38965-F65C-4792-B6D7-B77107DCB914"),
                    "Netgear Nighthawk X10 AD7200 Smart WiFi Router",
                    "www/packets/internet-routers/netgear-x10-ad7200.png")
            };
            var internetRouterId = Guid.Parse("59659676-8043-49FC-804D-1621650838C7");
            _internetRouterRepository.Get(internetRouterId)
                .Returns(internetRouters.Single(ir => ir.InternetRouterId == internetRouterId));

            var returnedInternetRouter = _internetRouterService.GetByInternetRouterId(internetRouterId);
            Assert.AreEqual(internetRouters.Single(ir => ir.InternetRouterId == internetRouterId).Name, returnedInternetRouter.Name);
        }
    }
}
