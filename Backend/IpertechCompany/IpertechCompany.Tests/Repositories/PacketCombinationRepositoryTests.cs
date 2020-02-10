using IpertechCompany.DbConnection;
using IpertechCompany.DbRepositories;
using IpertechCompany.IRepositories;
using IpertechCompany.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace IpertechCompany.Tests.Repositories
{
    [TestFixture]
    public class PacketCombinationRepositoryTests
    {
        private readonly IDbContext _dbContext;
        private readonly IPacketCombinationRepository _packetCombinationRepository;

        public PacketCombinationRepositoryTests()
        {
            _dbContext = new DbContext("Data Source=DESKTOP-883AG4N\\SQLEXPRESS;Initial Catalog=IpertechCompany;Integrated Security=True");
            _packetCombinationRepository = new PacketCombinationRepository(_dbContext);
        }


        [Test]
        public void GetAll_WithData_ReturnsListOfThreePacketCombinations()
        {
            Assert.AreEqual(3, _packetCombinationRepository.GetAll().Count());
        }

        [Test]
        public void Insert_WithRequiredFields_ReturnsListWithFourPacketCombinations()
        {
            var internetPacket = new InternetPacket(new Guid("D6435400-1891-4D54-B023-B62371A40C8E"), new InternetRouter(new Guid("59659676-8043-49FC-804D-1621650838C7")));
            var packetCombination = new PacketCombination(Guid.Empty, internetPacket, "Test Packet Combination");

            _packetCombinationRepository.Insert(packetCombination);
            Assert.AreEqual(4, _packetCombinationRepository.GetAll().Count());
        }

        [Test]
        public void Insert_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _packetCombinationRepository.Insert(null));
        }

        [Test]
        public void Update_WithRequiredFields_ReturnsPacketCombination()
        {
            var packetCombination = new PacketCombination
            {
                PacketCombinationId = Guid.Parse("78B15548-9DFA-4984-ACC8-70A5B801CA6D"),
                InternetPacket = new InternetPacket(Guid.Parse("D6435400-1891-4D54-B023-B62371A40C8E"),
                    new InternetRouter(Guid.Parse("59659676-8043-49FC-804D-1621650838C7"))),
                Name = "UPDATE"
            };

            _packetCombinationRepository.Update(packetCombination);
            var updatedPacketCombination = _packetCombinationRepository
                .GetAll().Single(pc => pc.PacketCombinationId == packetCombination.PacketCombinationId);

            Assert.AreEqual(packetCombination.Name, updatedPacketCombination.Name);
        }

        [Test]
        public void Update_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _packetCombinationRepository.Update(null));
        }

        [Test]
        public void Delete_WhichExists_ReturnsTrue()
        {
            Assert.True(_packetCombinationRepository.Delete(Guid.Parse("01131D91-9E30-4A42-A40B-7CE84A2B1413")));
        }

        [Test]
        public void Delete_WhichNotExists_ReturnsFalse()
        {
            Assert.False(_packetCombinationRepository.Delete(Guid.NewGuid()));
        }
    }
}
