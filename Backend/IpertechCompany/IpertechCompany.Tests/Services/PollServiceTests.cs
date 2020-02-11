using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using IpertechCompany.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IpertechCompany.Tests.Services
{
    [TestFixture]
    public class PollServiceTests
    {
        private readonly IPollRepository _pollRepository;
        private readonly IPollService _pollService;

        public PollServiceTests()
        {
            _pollRepository = Substitute.For<IPollRepository>();
            _pollService = new PollService(_pollRepository);
        }

        [Test]
        public void CreatePoll_NullObject_ExpectsException()
        {
            _pollRepository.Insert(null).Throws<NullReferenceException>();

            Assert.Throws<ArgumentNullException>(() => _pollService.CreatePoll(null));
            _pollRepository.DidNotReceive().Insert(null);
        }

        [Test]
        public void CreatePoll_WithoutRequiredFields_ExpectsException()
        {
            var poll = new Poll(Guid.NewGuid());
            _pollRepository.Insert(poll).Returns(poll);

            Assert.Throws<ArgumentException>(() => _pollService.CreatePoll(poll));
            _pollRepository.DidNotReceive().Insert(poll);
        }

        [Test]
        public void CreatePoll_WithRequiredFields_ReturnsPoll()
        {
            var poll = new Poll(Guid.Parse("BC8B7119-8C37-4034-9ABE-36E1334C86CE"),
                    "Da li ste zadovoljni brzinom interneta?", 0);
            _pollRepository.Insert(poll).Returns(poll);

            var returnedPoll = _pollService.CreatePoll(poll);
            _pollRepository.Received(1).Insert(poll);
            Assert.AreEqual(poll, returnedPoll);
        }

        [Test]
        public void UpdatePoll_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _pollService.UpdatePoll(null));
            _pollRepository.DidNotReceive().Update(null);
        }

        [Test]
        public void UpdatePoll_WithoutRequiredFields_ExpectsException()
        {
            var poll = new Poll();

            Assert.Throws<ArgumentException>(() => _pollService.UpdatePoll(poll));
            _pollRepository.DidNotReceive().Update(poll);
        }

        [Test]
        public void UpdatePoll_WithRequiredFields_ReturnsNothing()
        {
            var poll = new Poll(Guid.Parse("BC8B7119-8C37-4034-9ABE-36E1334C86CE"),
                    "Da li ste zadovoljni brzinom interneta?", 0);
            _pollService.UpdatePoll(poll);
            _pollRepository.Received(1).Update(poll);
        }

        [Test]
        public void DeletePoll_WhichNotExists_ReturnsTrue()
        {
            var pollId = Guid.NewGuid();

            _pollRepository.Delete(pollId).Returns(false);
            Assert.False(_pollService.DeletePoll(pollId));
        }

        [Test]
        public void DeletePoll_WhichExists_ReturnsTrue()
        {
            var pollId = Guid.Parse("BC8B7119-8C37-4034-9ABE-36E1334C86CE");

            _pollRepository.Delete(pollId).Returns(true);
            Assert.True(_pollService.DeletePoll(pollId));
        }

        [Test]
        public void GetPollByPollId_WithoutData_ReturnsEmptyObject()
        {
            var pollId = Guid.Parse("C8D6F372-06F8-40EE-8BC8-A1DFBFDC56FC");
            _pollRepository.Get(pollId)
                .Returns(new Poll());

            var returnedPoll = _pollService.GetByPollId(pollId);
            Assert.AreEqual(Guid.Empty, returnedPoll.PollId);
        }

        [Test]
        public void GetPollByPollId_WithData_ReturnsPoll()
        {
            var polls = new List<Poll>()
            {
                new Poll(Guid.Parse("BC8B7119-8C37-4034-9ABE-36E1334C86CE"),
                    "Da li ste zadovoljni brzinom interneta?", 0),
                new Poll(Guid.Parse("C764F6A5-84CC-4CAB-ACE4-B2AF703196C2"),
                    "Kako biste ocenili Vaše zadovoljstvo našim uslugama?", 2)
            };
            var pollId = Guid.Parse("BC8B7119-8C37-4034-9ABE-36E1334C86CE");
            _pollRepository.Get(pollId)
                .Returns(polls.Single(p => p.PollId == pollId));

            var returnedPoll = _pollService.GetByPollId(pollId);
            Assert.AreEqual(polls.Single(p => p.PollId == pollId).Question, returnedPoll.Question);
        }
    }
}
