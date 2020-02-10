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
    public class TvPacketTvChannelRepositoryTests
    {
        private readonly IDbContext _dbContext;
        private readonly ITvPacketTvChannelRepository _tvPacketTvChannelRepository;

        public TvPacketTvChannelRepositoryTests()
        {
            _dbContext = new DbContext("Data Source=DESKTOP-883AG4N\\SQLEXPRESS;Initial Catalog=IpertechCompany;Integrated Security=True");
            _tvPacketTvChannelRepository = new TvPacketTvChannelRepository(_dbContext);
        }

        [Test]
        public void GetByTvPacketId_WithExistingTvPacketTvChannelType_ReturnsPopulatedList()
        {
            Assert.AreEqual(21, _tvPacketTvChannelRepository.Get(Guid.Parse("6FC42F38-929A-4CD2-BE56-5F96246D19C3")).Count());
        }

        [Test]
        public void GetByTvPacketId_WithoutExistingTvPacketId_ReturnsEmptyList()
        {
            Assert.AreEqual(0, _tvPacketTvChannelRepository.Get(Guid.NewGuid()).Count());
        }

        [Test]
        public void Insert_WithRequiredFields_ReturnsEighteenTvPacketTvChannel()
        {
            var tvPacketTvChannel = new TvPacketTvChannel(new TvPacket(Guid.Parse("D928DB62-A4C9-492E-BBDB-B15EF6E9E907")), new TvChannel(Guid.Parse("A738A892-F92B-4263-AB5C-E45CA3F0639F")));

            _tvPacketTvChannelRepository.Insert(tvPacketTvChannel);
            Assert.AreEqual(18, _tvPacketTvChannelRepository.Get(Guid.Parse("D928DB62-A4C9-492E-BBDB-B15EF6E9E907")).Count());
        }

        [Test]
        public void Insert_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _tvPacketTvChannelRepository.Insert(null));
        }

        [Test]
        public void Delete_WhichExists_ReturnsTrue()
        {
            var tvPacketTvChannel =
                new TvPacketTvChannel(new TvPacket(Guid.Parse("D928DB62-A4C9-492E-BBDB-B15EF6E9E907")),
                    new TvChannel(Guid.Parse("F0DBF98A-A5BE-4866-B6D0-48261C29926A")));
            Assert.True(_tvPacketTvChannelRepository.Delete(tvPacketTvChannel));
        }

        [Test]
        public void Delete_WhichNotExists_ReturnsFalse()
        {
            var tvPacketTvChannel = new TvPacketTvChannel(new TvPacket(Guid.NewGuid()), new TvChannel(Guid.NewGuid()));
            Assert.False(_tvPacketTvChannelRepository.Delete(tvPacketTvChannel));
        }
    }
}
