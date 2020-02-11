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
    public class TvChannelServiceTests
    {
        private readonly ITvChannelRepository _tvChannelRepository;
        private readonly ITvChannelService _tvChannelService;

        public TvChannelServiceTests()
        {
            _tvChannelRepository = Substitute.For<ITvChannelRepository>();
            _tvChannelService = new TvChannelService(_tvChannelRepository);
        }

        [Test]
        public void CreateTvChannel_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _tvChannelService.CreateTvChannel(null));
            _tvChannelRepository.DidNotReceive().Insert(null);
        }

        [Test]
        public void CreateTvChannel_WithoutRequitedFields_ExpectsException()
        {
            var tvChannel = new TvChannel();

            Assert.Throws<ArgumentException>(() => _tvChannelService.CreateTvChannel(tvChannel));
            _tvChannelRepository.DidNotReceive().Insert(tvChannel);
        }

        [Test]
        public void CreateTvChannel_WithRequiredFields_ReturnsTvChannel()
        {
            var tvChannel = new TvChannel(Guid.Parse("90511DBC-A921-4858-9D4E-1439E681EE9E"), "RTS 2", "www/tv-packets/tv-channels/rts-2.png", 4, true);
            _tvChannelRepository.Insert(tvChannel).Returns(tvChannel);

            var returnedTvChannel = _tvChannelService.CreateTvChannel(tvChannel);
            _tvChannelRepository.Received(1).Insert(tvChannel);
            Assert.AreEqual(tvChannel, returnedTvChannel);
        }

        [Test]
        public void UpdateTvChannel_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _tvChannelService.UpdateTvChannel(null));
            _tvChannelRepository.DidNotReceive().Update(null);
        }

        [Test]
        public void UpdateTvChannel_WithoutRequiredFields_ExpectsException()
        {
            var tvChannel = new TvChannel();

            Assert.Throws<ArgumentException>(() => _tvChannelService.UpdateTvChannel(tvChannel));
            _tvChannelRepository.DidNotReceive().Update(tvChannel);
        }

        [Test]
        public void UpdateTvChannel_WithRequiredFields_ReturnsNothing()
        {
            var tvChannel = new TvChannel(Guid.Parse("90511DBC-A921-4858-9D4E-1439E681EE9E"), "RTS 2", "www/tv-packets/tv-channels/rts-2.png", 4, true);
            _tvChannelService.UpdateTvChannel(tvChannel);
            _tvChannelRepository.Received(1).Update(tvChannel);
        }

        [Test]
        public void GetAllTvChannels_WithoutData_ReturnsEmptyList()
        {
            _tvChannelRepository.GetAll().Returns(new List<TvChannel>());
            Assert.AreEqual(0, _tvChannelService.GetAllTvChannels().Count());
        }

        [Test]
        public void GetAllTvChannels_WithData_ReturnsPopulatedList()
        {
            var tvChannels = new List<TvChannel>()
            {
                new TvChannel(Guid.Parse("90511DBC-A921-4858-9D4E-1439E681EE9E"), "RTS 2", "www/tv-packets/tv-channels/rts-2.png", 4, true),
                new TvChannel(Guid.Parse("2E7A9226-3949-4DEB-927E-1E30BE88F815"), "RTS 3", "www/tv-packets/tv-channels/rts-3.png", 5, true),
                new TvChannel(Guid.Parse("2D7C7FC9-E323-43D1-B917-28AC577839C3"), "O2.TV", "www/tv-packets/tv-channels/02-tv.png", 6, true),
                new TvChannel(Guid.Parse("737E40CC-FD18-4453-8154-2FE4F7DB5D12"), "Prva", "www/tv-packets/tv-channels/prva.png", 7,
                true),
                new TvChannel(Guid.Parse("AC16EF24-F167-442F-A76E-3592C66A5683"), "Studio B", "www/tv-packets/tv-channels/studio-b.png", 25, false)
            };
            _tvChannelRepository.GetAll().Returns(tvChannels);

            Assert.AreEqual(tvChannels.Count, _tvChannelService.GetAllTvChannels().Count());
        }
    }
}
