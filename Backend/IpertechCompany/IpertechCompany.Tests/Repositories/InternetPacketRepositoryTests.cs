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
    public class InternetPacketRepositoryTests
    {
        private readonly IDbContext _dbContext;
        private readonly IInternetPacketRepository _internetPacketRepository;

        public InternetPacketRepositoryTests()
        {
            _dbContext = new DbContext("Data Source=DESKTOP-883AG4N\\SQLEXPRESS;Initial Catalog=IpertechCompany;Integrated Security=True");
            _internetPacketRepository = new InternetPacketRepository(_dbContext);
        }

        [Test]
        public void GetAll_WithData_ReturnsListOfThreeInternetPackets()
        {
            Assert.AreEqual(3, _internetPacketRepository.GetAll().Count());
        }

        [Test]
        public void Insert_WithRequiredFields_ReturnsListWithFourInternetPackets()
        {
            var internetPacket = new InternetPacket(new Guid(), new InternetRouter(new Guid("59659676-8043-49FC-804D-1621650838C7")), "Insert", "", 0);

            _internetPacketRepository.Insert(internetPacket);
            Assert.AreEqual(4, _internetPacketRepository.GetAll().Count());
        }

        [Test]
        public void Insert_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _internetPacketRepository.Insert(null));
        }

        [Test]
        public void Update_WithRequiredFields_ReturnsInternetPacket()
        {
            var internetPacket = new InternetPacket(Guid.Parse("D6435400-1891-4D54-B023-B62371A40C8E"), new InternetRouter(Guid.Parse("59659676-8043-49FC-804D-1621650838C7")), "Update", "", 0);

            _internetPacketRepository.Update(internetPacket);
            var updatedInternetPacket = _internetPacketRepository
                .GetAll().Single(ip => ip.Name == internetPacket.Name);

            Assert.AreEqual(internetPacket.Name, updatedInternetPacket.Name);
        }

        [Test]
        public void Update_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _internetPacketRepository.Update(null));
        }

        [Test]
        public void Delete_WhichExists_ReturnsTrue()
        {
            Assert.True(_internetPacketRepository.Delete(Guid.Parse("03ECCAD9-F9C6-4814-8491-CFBF11161562"), Guid.Parse("66C38965-F65C-4792-B6D7-B77107DCB914")));
        }

        [Test]
        public void Delete_WhichNotExists_ReturnsFalse()
        {
            Assert.False(_internetPacketRepository.Delete(Guid.NewGuid(), Guid.NewGuid()));
        }
    }
}
