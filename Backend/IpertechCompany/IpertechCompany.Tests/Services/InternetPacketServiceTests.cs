using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using IpertechCompany.DbConnection;
using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using IpertechCompany.Services;
using NSubstitute;
using NUnit.Framework;

namespace IpertechCompany.Tests.Services
{
    [TestFixture]
    public class InternetPacketServiceTests
    {
        private readonly IInternetPacketRepository _internetPacketRepository;
        private readonly IInternetPacketService _internetPacketService;

        public InternetPacketServiceTests()
        {
            _internetPacketRepository = Substitute.For<IInternetPacketRepository>();
            _internetPacketService = new InternetPacketService(_internetPacketRepository);
        }

        [Test]
        public void CreateInternetPacket_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _internetPacketService.CreateInternetPacket(null));
            _internetPacketRepository.DidNotReceive().Insert(null);
        }

        [Test]
        public void CreateInternetPacket_WithoutRequitedFields_ExpectsException()
        {
            var internetPacket = new InternetPacket();

            Assert.Throws<ArgumentException>(() => _internetPacketService.CreateInternetPacket(internetPacket));
            _internetPacketRepository.DidNotReceive().Insert(internetPacket);
        }

        [Test]
        public void CreateInternetPacket_WithRequiredFields_ReturnsInternetPacket()
        {
            var internetPacket = new InternetPacket(Guid.Parse("D6435400-1891-4D54-B023-B62371A40C8E"),
                new InternetRouter(Guid.Parse("59659676-8043-49FC-804D-1621650838C7")), "Net Start", "40/5 Mbps",
                Convert.ToDecimal(1199.99));
            _internetPacketRepository.Insert(internetPacket).Returns(internetPacket);

            var returnedInternetPacket = _internetPacketService.CreateInternetPacket(internetPacket);
            _internetPacketRepository.Received(1).Insert(internetPacket);
            Assert.AreEqual(internetPacket, returnedInternetPacket);
        }

        [Test]
        public void UpdateInternetPacket_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _internetPacketService.UpdateInternetPacket(null));
            _internetPacketRepository.DidNotReceive().Update(null);
        }

        [Test]
        public void UpdateInternetPacket_WithoutRequiredFields_ExpectsException()
        {
            var internetPacket = new InternetPacket();

            Assert.Throws<ArgumentException>(() => _internetPacketService.UpdateInternetPacket(internetPacket));
            _internetPacketRepository.DidNotReceive().Update(internetPacket);
        }

        [Test]
        public void UpdateInternetPacket_WithRequiredFields_ReturnsNothing()
        {
            var internetPacket = new InternetPacket(Guid.Parse("D6435400-1891-4D54-B023-B62371A40C8E"),
                new InternetRouter(Guid.Parse("59659676-8043-49FC-804D-1621650838C7")), "Net Start", "40/5 Mbps",
                Convert.ToDecimal(1199.99));
            _internetPacketService.UpdateInternetPacket(internetPacket);
            _internetPacketRepository.Received(1).Update(internetPacket);
        }

        [Test]
        public void GetAllInternetPackets_WithoutData_ReturnsEmptyList()
        {
            _internetPacketRepository.GetAll().Returns(new List<InternetPacket>());
            Assert.AreEqual(0, _internetPacketService.GetAllInternetPackets().Count());
        }

        [Test]
        public void GetAllInternetPackets_WithData_ReturnsPopulatedList()
        {
            var internetPackets = new List<InternetPacket>()
            {
                new InternetPacket(Guid.Parse("D6435400-1891-4D54-B023-B62371A40C8E"), new InternetRouter(Guid.Parse("59659676-8043-49FC-804D-1621650838C7")), "Net Start", "40/5 Mbps"),
                new InternetPacket(Guid.Parse("B1774C05-62D6-4B98-8E90-C8C0A4951F49"), new InternetRouter(Guid.Parse("66C38965-F65C-4792-B6D7-B77107DCB914")), "Net Proffesional", "100Mb/15Mb"),
                new InternetPacket(Guid.Parse("03ECCAD9-F9C6-4814-8491-CFBF11161562"), new InternetRouter(Guid.Parse("66C38965-F65C-4792-B6D7-B77107DCB914")), "TEST", "200Mb/20Mb"),
                new InternetPacket(Guid.Parse("23DFB2B6-863D-4410-8B0C-F8D05F629F42"), new InternetRouter(Guid.Parse("32278526-B2EA-44E6-A5C8-49C4AAE6CA2E")), "Net Home", "80Mb/8Mb")
            };
            _internetPacketRepository.GetAll().Returns(internetPackets);

            Assert.AreEqual(internetPackets.Count, _internetPacketService.GetAllInternetPackets().Count());
        }
    }
}
