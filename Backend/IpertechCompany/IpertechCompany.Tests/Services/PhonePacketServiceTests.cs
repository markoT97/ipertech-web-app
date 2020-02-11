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
    public class PhonePacketServiceTests
    {
        private readonly IPhonePacketRepository _phonePacketRepository;
        private readonly IPhonePacketService _phonePacketService;

        public PhonePacketServiceTests()
        {
            _phonePacketRepository = Substitute.For<IPhonePacketRepository>();
            _phonePacketService = new PhonePacketService(_phonePacketRepository);
        }

        [Test]
        public void CreatePhonePacket_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _phonePacketService.CreatePhonePacket(null));
            _phonePacketRepository.DidNotReceive().Insert(null);
        }

        [Test]
        public void CreatePhonePacket_WithoutRequitedFields_ExpectsException()
        {
            var phonePacket = new PhonePacket();

            Assert.Throws<ArgumentException>(() => _phonePacketService.CreatePhonePacket(phonePacket));
            _phonePacketRepository.DidNotReceive().Insert(phonePacket);
        }

        [Test]
        public void CreatePhonePacket_WithRequiredFields_ReturnsPhonePacket()
        {
            var phonePacket = new PhonePacket(Guid.Parse("ED88305F-97AB-41C5-AD43-820D679EF868"), "Phone Bronze", 0, Convert.ToDecimal(899.54));
            _phonePacketRepository.Insert(phonePacket).Returns(phonePacket);

            var returnedPhonePacket = _phonePacketService.CreatePhonePacket(phonePacket);
            _phonePacketRepository.Received(1).Insert(phonePacket);
            Assert.AreEqual(phonePacket, returnedPhonePacket);
        }

        [Test]
        public void UpdatePhonePacket_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _phonePacketService.UpdatePhonePacket(null));
            _phonePacketRepository.DidNotReceive().Update(null);
        }

        [Test]
        public void UpdatePhonePacket_WithoutRequiredFields_ExpectsException()
        {
            var phonePacket = new PhonePacket();

            Assert.Throws<ArgumentException>(() => _phonePacketService.UpdatePhonePacket(phonePacket));
            _phonePacketRepository.DidNotReceive().Update(phonePacket);
        }

        [Test]
        public void UpdatePhonePacket_WithRequiredFields_ReturnsNothing()
        {
            var phonePacket = new PhonePacket(Guid.Parse("ED88305F-97AB-41C5-AD43-820D679EF868"), "Phone Bronze", 0, Convert.ToDecimal(899.54));
            _phonePacketService.UpdatePhonePacket(phonePacket);
            _phonePacketRepository.Received(1).Update(phonePacket);
        }

        [Test]
        public void GetAllPhonePackets_WithoutData_ReturnsEmptyList()
        {
            _phonePacketRepository.GetAll().Returns(new List<PhonePacket>());
            Assert.AreEqual(0, _phonePacketService.GetAllPhonePackets().Count());
        }

        [Test]
        public void GetAllPhonePackets_WithData_ReturnsPopulatedList()
        {
            var phonePackets = new List<PhonePacket>()
            {
                new PhonePacket(Guid.Parse("ED88305F-97AB-41C5-AD43-820D679EF868"), "Phone Bronze", 0, Convert.ToDecimal(899.54)),
                new PhonePacket(Guid.Parse("4157F23A-CDBA-4CD4-ACD7-9222B2A4034B"), "Phone Silver", 60, Convert.ToDecimal(1099.54)),
                new PhonePacket(Guid.Parse("00DA2845-C2AD-4893-9DAB-B844D99ED1EF"), "Phone Test", 100, Convert.ToDecimal(1399.54)),
                new PhonePacket(Guid.Parse("1CF314E2-4CD3-429E-BA43-D32F19937BE7"), "Phone Gold", 200, Convert.ToDecimal(1299.54))
            };
            _phonePacketRepository.GetAll().Returns(phonePackets);

            Assert.AreEqual(phonePackets.Count, _phonePacketService.GetAllPhonePackets().Count());
        }
    }
}
