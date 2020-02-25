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
    public class TvPacketTvChannelTvChannelServiceTests
    {
        private readonly ITvPacketTvChannelRepository _tvPacketTvChannelRepository;
        private readonly ITvPacketTvChannelService _tvPacketTvChannelService;

        public TvPacketTvChannelTvChannelServiceTests()
        {
            _tvPacketTvChannelRepository = Substitute.For<ITvPacketTvChannelRepository>();
            _tvPacketTvChannelService = new TvPacketTvChannelService(_tvPacketTvChannelRepository);
        }

        [Test]
        public void CreateTvPacketTvChannel_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _tvPacketTvChannelService.CreateTvPacketTvChannel(null));
            _tvPacketTvChannelRepository.DidNotReceive().Insert(null);
        }

        [Test]
        public void CreateTvPacketTvChannel_WithoutRequitedFields_ExpectsException()
        {
            var tvPacketTvChannelTvChannel = new TvPacketTvChannel();

            Assert.Throws<ArgumentException>(() => _tvPacketTvChannelService.CreateTvPacketTvChannel(tvPacketTvChannelTvChannel));
            _tvPacketTvChannelRepository.DidNotReceive().Insert(tvPacketTvChannelTvChannel);
        }

        [Test]
        public void CreateTvPacketTvChannel_WithRequiredFields_ReturnsTvPacketTvChannel()
        {
            var tvPacketTvChannel = new TvPacketTvChannel(new TvPacket(Guid.Parse("0368FC73-0AAD-42F3-AF59-2A419060BE0E")), new TvChannel(Guid.Parse("90511DBC-A921-4858-9D4E-1439E681EE9E")));
            _tvPacketTvChannelRepository.Insert(tvPacketTvChannel).Returns(tvPacketTvChannel);

            var returnedTvPacketTvChannel = _tvPacketTvChannelService.CreateTvPacketTvChannel(tvPacketTvChannel);
            _tvPacketTvChannelRepository.Received(1).Insert(tvPacketTvChannel);
            Assert.AreEqual(tvPacketTvChannel, returnedTvPacketTvChannel);
        }

        [Test]
        public void GetTvChannelsByTvPacketId_WithoutData_ReturnsEmptyList()
        {
            var tvPacketId = Guid.Parse("6FC42F38-929A-4CD2-BE56-5F96246D19C3");
            _tvPacketTvChannelRepository.Get(tvPacketId)
                .Returns(new List<TvChannel>());

            var returnedTvChannels = _tvPacketTvChannelService.GetTvChannelsByTvPacketId(tvPacketId);
            Assert.AreEqual(0, returnedTvChannels.Count());
        }

        [Test]
        public void GetTvChannelsByTvPacketId_WithData_ReturnsThreeChannels()
        {
            var tvPacketsTvChannels = new List<TvPacketTvChannel>()
            {
                new TvPacketTvChannel(new TvPacket(Guid.Parse("6FC42F38-929A-4CD2-BE56-5F96246D19C3")), new TvChannel(Guid.Parse("90511DBC-A921-4858-9D4E-1439E681EE9E"))),
                new TvPacketTvChannel(new TvPacket(Guid.Parse("6FC42F38-929A-4CD2-BE56-5F96246D19C3")), new TvChannel(Guid.Parse("2E7A9226-3949-4DEB-927E-1E30BE88F815"))),
                new TvPacketTvChannel(new TvPacket(Guid.Parse("6FC42F38-929A-4CD2-BE56-5F96246D19C3")), new TvChannel(Guid.Parse("2D7C7FC9-E323-43D1-B917-28AC577839C3")))
            };

            var allTvChannels = new List<TvChannel>()
            {
                new TvChannel(Guid.Parse("90511DBC-A921-4858-9D4E-1439E681EE9E"), "RTS 2", "www/tv-packets/tv-channels/rts-2.png", 4, true),
                new TvChannel(Guid.Parse("2E7A9226-3949-4DEB-927E-1E30BE88F815"), "RTS 3", "www/tv-packets/tv-channels/rts-3.png", 5, true),
               new TvChannel(Guid.Parse("2D7C7FC9-E323-43D1-B917-28AC577839C3"), "O2.TV", "www/tv-packets/tv-channels/o2-tv.png", 6, true),
               new TvChannel(Guid.Parse("BE021B37-2B18-4634-8392-6B40DEEB8238"), "Studio B", "www/tv-packets/tv-channels/studio-b.png", 12, true)
            };

            var tvChannelsByTvPacketId = new List<TvChannel>();
            var tvPacketId = Guid.Parse("6FC42F38-929A-4CD2-BE56-5F96246D19C3");

            foreach (var tvPacketTvChannel in tvPacketsTvChannels)
            {
                foreach (var tvChannel in allTvChannels)
                {
                    if (tvPacketTvChannel.TvPacket.TvPacketId.Equals(tvPacketId))
                    {
                        tvChannelsByTvPacketId.Add(tvChannel);
                    }
                }
            }

            _tvPacketTvChannelRepository.Get(tvPacketId)
                .Returns(tvChannelsByTvPacketId);

            var returnedTvChannels = _tvPacketTvChannelService.GetTvChannelsByTvPacketId(tvPacketId);
            Assert.AreEqual(tvChannelsByTvPacketId.Count(), returnedTvChannels.Count());
        }
    }
}
