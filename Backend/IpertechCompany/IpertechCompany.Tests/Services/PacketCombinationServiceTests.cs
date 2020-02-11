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
    public class PacketCombinationServiceTests
    {
        private readonly IPacketCombinationRepository _packetCombinationRepository;
        private readonly IPacketCombinationService _packetCombinationService;

        public PacketCombinationServiceTests()
        {
            _packetCombinationRepository = Substitute.For<IPacketCombinationRepository>();
            _packetCombinationService = new PacketCombinationService(_packetCombinationRepository);
        }

        [Test]
        public void CreatePacketCombination_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _packetCombinationService.CreatePacketCombination(null));
            _packetCombinationRepository.DidNotReceive().Insert(null);
        }

        [Test]
        public void CreatePacketCombination_WithoutRequitedFields_ExpectsException()
        {
            var packetCombination = new PacketCombination();

            Assert.Throws<ArgumentException>(() => _packetCombinationService.CreatePacketCombination(packetCombination));
            _packetCombinationRepository.DidNotReceive().Insert(packetCombination);
        }

        [Test]
        public void CreatePacketCombination_WithRequiredFields_ReturnsPacketCombination()
        {
            var packetCombination = new PacketCombination(Guid.Parse("78B15548-9DFA-4984-ACC8-70A5B801CA6D"), new InternetPacket(Guid.Parse("D6435400-1891-4D54-B023-B62371A40C8E"), new InternetRouter(Guid.Parse("59659676-8043-49FC-804D-1621650838C7"))), "Solo1");
            _packetCombinationRepository.Insert(packetCombination).Returns(packetCombination);

            var returnedPacketCombination = _packetCombinationService.CreatePacketCombination(packetCombination);
            _packetCombinationRepository.Received(1).Insert(packetCombination);
            Assert.AreEqual(packetCombination, returnedPacketCombination);
        }

        [Test]
        public void UpdatePacketCombination_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _packetCombinationService.UpdatePacketCombination(null));
            _packetCombinationRepository.DidNotReceive().Update(null);
        }

        [Test]
        public void UpdatePacketCombination_WithoutRequiredFields_ExpectsException()
        {
            var packetCombination = new PacketCombination();

            Assert.Throws<ArgumentException>(() => _packetCombinationService.UpdatePacketCombination(packetCombination));
            _packetCombinationRepository.DidNotReceive().Update(packetCombination);
        }

        [Test]
        public void UpdatePacketCombination_WithRequiredFields_ReturnsNothing()
        {
            var packetCombination = new PacketCombination(Guid.Parse("78B15548-9DFA-4984-ACC8-70A5B801CA6D"), new InternetPacket(Guid.Parse("D6435400-1891-4D54-B023-B62371A40C8E"), new InternetRouter(Guid.Parse("59659676-8043-49FC-804D-1621650838C7"))), "Solo1");
            _packetCombinationService.UpdatePacketCombination(packetCombination);
            _packetCombinationRepository.Received(1).Update(packetCombination);
        }

        [Test]
        public void GetAllPacketCombinations_WithoutData_ReturnsEmptyList()
        {
            _packetCombinationRepository.GetAll().Returns(new List<PacketCombination>());
            Assert.AreEqual(0, _packetCombinationService.GetAllPacketCombinations().Count());
        }

        [Test]
        public void GetAllPacketCombinations_WithData_ReturnsPopulatedList()
        {
            var packetCombinations = new List<PacketCombination>()
            {
                new PacketCombination(Guid.Parse("78B15548-9DFA-4984-ACC8-70A5B801CA6D"), new InternetPacket(Guid.Parse("D6435400-1891-4D54-B023-B62371A40C8E"), new InternetRouter(Guid.Parse("59659676-8043-49FC-804D-1621650838C7"))), "Solo1"),
                new PacketCombination(Guid.Parse("01131D91-9E30-4A42-A40B-7CE84A2B1413"), new InternetPacket(Guid.Parse("23DFB2B6-863D-4410-8B0C-F8D05F629F42"), new InternetRouter(Guid.Parse("32278526-B2EA-44E6-A5C8-49C4AAE6CA2E"))), "Solo2"),
                new PacketCombination(Guid.Parse("2D03F2E9-0E5B-4562-AC71-C08755188AD1"), new InternetPacket(Guid.Parse("B1774C05-62D6-4B98-8E90-C8C0A4951F49"), new InternetRouter(Guid.Parse("66C38965-F65C-4792-B6D7-B77107DCB914"))), "Solo3"),
                new PacketCombination(Guid.Parse("1B25A58C-24D9-4B3A-8518-E9201B70E6A9"), new InternetPacket(Guid.Parse("23DFB2B6-863D-4410-8B0C-F8D05F629F42"), new InternetRouter(Guid.Parse("32278526-B2EA-44E6-A5C8-49C4AAE6CA2E"))), "Duo22", new TvPacket(Guid.Parse("9EB8E21E-826F-4BE1-A74C-864242E86636")))
            };
            _packetCombinationRepository.GetAll().Returns(packetCombinations);

            Assert.AreEqual(packetCombinations.Count, _packetCombinationService.GetAllPacketCombinations().Count());
        }
    }
}
