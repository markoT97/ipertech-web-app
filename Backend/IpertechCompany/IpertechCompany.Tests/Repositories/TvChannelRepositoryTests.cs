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
    public class TvChannelRepositoryTests
    {
        private readonly IDbContext _dbContext;
        private readonly ITvChannelRepository _tvChannelRepository;

        public TvChannelRepositoryTests()
        {
            _dbContext = new DbContext("Data Source=DESKTOP-883AG4N\\SQLEXPRESS;Initial Catalog=IpertechCompany;Integrated Security=True");
            _tvChannelRepository = new TvChannelRepository(_dbContext);
        }


        [Test]
        public void GetAll_WithData_ReturnsListOfTwentyOneTvChannels()
        {
            Assert.AreEqual(22, _tvChannelRepository.GetAll().Count());
        }

        [Test]
        public void Insert_WithRequiredFields_ReturnsListWithTwentyTwoTvChannels()
        {
            var tvChannel = new TvChannel(Guid.NewGuid(), "Insert Name", "Insert Location", 99, true);

            _tvChannelRepository.Insert(tvChannel);
            Assert.AreEqual(23, _tvChannelRepository.GetAll().Count());
        }

        [Test]
        public void Insert_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _tvChannelRepository.Insert(null));
        }

        [Test]
        public void Update_WithRequiredFields_ReturnsTvChannel()
        {
            var tvChannel = new TvChannel
            {
                TvChannelId = Guid.Parse("958ED64A-A724-4A7D-BE15-5CC0C9AA8132"),
                Name = "Update Name",
                ImageLocation = "Update Location",
                PositionNumber = 100,
                TvBackwards = true
            };

            _tvChannelRepository.Update(tvChannel);
            var updatedTvChannel = _tvChannelRepository
                .GetAll().Single(pc => pc.TvChannelId == tvChannel.TvChannelId);

            Assert.AreEqual(tvChannel.Name, updatedTvChannel.Name);
        }

        [Test]
        public void Update_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _tvChannelRepository.Update(null));
        }

        [Test]
        public void Delete_WhichExists_ReturnsTrue()
        {
            Assert.True(_tvChannelRepository.Delete(Guid.Parse("6EC593F8-4AEB-4D7C-93B4-D7957C1ACBCE")));
        }

        [Test]
        public void Delete_WhichNotExists_ReturnsFalse()
        {
            Assert.False(_tvChannelRepository.Delete(Guid.NewGuid()));
        }
    }
}
