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
    public class TvPacketRepositoryTests
    {
        private readonly IDbContext _dbContext;
        private readonly ITvPacketRepository _tvPacketRepository;

        public TvPacketRepositoryTests()
        {
            _dbContext = new DbContext("Data Source=DESKTOP-883AG4N\\SQLEXPRESS;Initial Catalog=IpertechCompany;Integrated Security=True");
            _tvPacketRepository = new TvPacketRepository(_dbContext);
        }


        [Test]
        public void GetAll_WithData_ReturnsListOfThreeTvPackets()
        {
            Assert.AreEqual(3, _tvPacketRepository.GetAll().Count());
        }

        [Test]
        public void Insert_WithRequiredFields_ReturnsListWithFourTvPackets()
        {
            var tvPacket = new TvPacket(Guid.NewGuid(), "Insert Name", 1000);

            _tvPacketRepository.Insert(tvPacket);
            Assert.AreEqual(4, _tvPacketRepository.GetAll().Count());
        }

        [Test]
        public void Insert_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _tvPacketRepository.Insert(null));
        }

        [Test]
        public void Update_WithRequiredFields_ReturnsTvPacket()
        {
            var tvPacket = new TvPacket
            {
                TvPacketId = Guid.Parse("D928DB62-A4C9-492E-BBDB-B15EF6E9E907"),
                Name = "Update Name",
                Price = 4000
            };

            _tvPacketRepository.Update(tvPacket);
            var updatedTvPacket = _tvPacketRepository
                .GetAll().Single(tp => tp.TvPacketId == tvPacket.TvPacketId);

            Assert.AreEqual(tvPacket.Name, updatedTvPacket.Name);
        }

        [Test]
        public void Update_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _tvPacketRepository.Update(null));
        }

        [Test]
        public void Delete_WhichExists_ReturnsTrue()
        {
            Assert.True(_tvPacketRepository.Delete(Guid.Parse("BB0D6A7A-7464-46E8-A446-FE9747FAA1B4")));
        }

        [Test]
        public void Delete_WhichNotExists_ReturnsFalse()
        {
            Assert.False(_tvPacketRepository.Delete(Guid.NewGuid()));
        }
    }
}
