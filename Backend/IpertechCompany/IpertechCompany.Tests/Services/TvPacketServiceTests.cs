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
    public class TvPacketServiceTests
    {
        private readonly ITvPacketRepository _tvPacketRepository;
        private readonly ITvPacketService _tvPacketService;

        public TvPacketServiceTests()
        {
            _tvPacketRepository = Substitute.For<ITvPacketRepository>();
            _tvPacketService = new TvPacketService(_tvPacketRepository);
        }

        [Test]
        public void CreateTvPacket_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _tvPacketService.CreateTvPacket(null));
            _tvPacketRepository.DidNotReceive().Insert(null);
        }

        [Test]
        public void CreateTvPacket_WithoutRequitedFields_ExpectsException()
        {
            var tvPacket = new TvPacket();

            Assert.Throws<ArgumentException>(() => _tvPacketService.CreateTvPacket(tvPacket));
            _tvPacketRepository.DidNotReceive().Insert(tvPacket);
        }

        [Test]
        public void CreateTvPacket_WithRequiredFields_ReturnsTvPacket()
        {
            var tvPacket = new TvPacket(Guid.Parse("6FC42F38-929A-4CD2-BE56-5F96246D19C3"), "TV Starter", Convert.ToDecimal(1587.99));
            _tvPacketRepository.Insert(tvPacket).Returns(tvPacket);

            var returnedTvPacket = _tvPacketService.CreateTvPacket(tvPacket);
            _tvPacketRepository.Received(1).Insert(tvPacket);
            Assert.AreEqual(tvPacket, returnedTvPacket);
        }

        [Test]
        public void UpdateTvPacket_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _tvPacketService.UpdateTvPacket(null));
            _tvPacketRepository.DidNotReceive().Update(null);
        }

        [Test]
        public void UpdateTvPacket_WithoutRequiredFields_ExpectsException()
        {
            var tvPacket = new TvPacket();

            Assert.Throws<ArgumentException>(() => _tvPacketService.UpdateTvPacket(tvPacket));
            _tvPacketRepository.DidNotReceive().Update(tvPacket);
        }

        [Test]
        public void UpdateTvPacket_WithRequiredFields_ReturnsNothing()
        {
            var tvPacket = new TvPacket(Guid.Parse("6FC42F38-929A-4CD2-BE56-5F96246D19C3"), "TV Starter", Convert.ToDecimal(1587.99));
            _tvPacketService.UpdateTvPacket(tvPacket);
            _tvPacketRepository.Received(1).Update(tvPacket);
        }

        [Test]
        public void GetAllTvPackets_WithoutData_ReturnsEmptyList()
        {
            _tvPacketRepository.GetAll().Returns(new List<TvPacket>());
            Assert.AreEqual(0, _tvPacketService.GetAllTvPackets().Count());
        }

        [Test]
        public void GetAllTvPackets_WithData_ReturnsPopulatedList()
        {
            var tvPackets = new List<TvPacket>()
            {
                new TvPacket(Guid.Parse("6FC42F38-929A-4CD2-BE56-5F96246D19C3"), "TV Starter", Convert.ToDecimal(1587.99)),
                new TvPacket(Guid.Parse("9EB8E21E-826F-4BE1-A74C-864242E86636"), "TV Intermediate", Convert.ToDecimal(2124.99)),
                new TvPacket(Guid.Parse("D928DB62-A4C9-492E-BBDB-B15EF6E9E907"), "TV Advanced", Convert.ToDecimal(2888.99))
            };
            _tvPacketRepository.GetAll().Returns(tvPackets);

            Assert.AreEqual(tvPackets.Count, _tvPacketService.GetAllTvPackets().Count());
        }
    }
}
