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
    public class PhonePacketRepositoryTests
    {
        private readonly IDbContext _dbContext;
        private readonly IPhonePacketRepository _phonePacketRepository;

        public PhonePacketRepositoryTests()
        {
            _dbContext = new DbContext("Data Source=DESKTOP-883AG4N\\SQLEXPRESS;Initial Catalog=IpertechCompany;Integrated Security=True");
            _phonePacketRepository = new PhonePacketRepository(_dbContext);
        }
        

        [Test]
        public void GetAll_WithData_ReturnsListOfThreePhonePackets()
        {
            Assert.AreEqual(3, _phonePacketRepository.GetAll().Count());
        }

        [Test]
        public void Insert_WithRequiredFields_ReturnsListWithFourPhonePackets()
        {
            var phonePacket = new PhonePacket(Guid.NewGuid(), "Insert Name", 10, 1000);

            _phonePacketRepository.Insert(phonePacket);
            Assert.AreEqual(4, _phonePacketRepository.GetAll().Count());
        }

        [Test]
        public void Insert_NullObject_ThrowsException()
        {
            Assert.Throws<NullReferenceException>(() => _phonePacketRepository.Insert(null));
        }

        [Test]
        public void Update_WithRequiredFields_ReturnsPhonePacket()
        {
            var phonePacket = new PhonePacket
            {
                PhonePacketId = Guid.Parse("ED88305F-97AB-41C5-AD43-820D679EF868"),
                Name = "Update Name",
                FreeMinutes = 10
            };

            _phonePacketRepository.Update(phonePacket);
            var updatedPhonePacket = _phonePacketRepository
                .GetAll().Single(pp => pp.PhonePacketId == phonePacket.PhonePacketId);

            Assert.AreEqual(phonePacket.Name, updatedPhonePacket.Name);
        }

        [Test]
        public void Update_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _phonePacketRepository.Update(null));
        }

        [Test]
        public void Delete_WhichExists_ReturnsTrue()
        {
            Assert.True(_phonePacketRepository.Delete(Guid.Parse("00DA2845-C2AD-4893-9DAB-B844D99ED1EF")));
        }

        [Test]
        public void Delete_WhichNotExists_ReturnsFalse()
        {
            Assert.False(_phonePacketRepository.Delete(Guid.NewGuid()));
        }
    }
}
