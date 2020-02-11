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
    public class PollOptionServiceTests
    {
        private readonly IPollOptionRepository _pollOptionRepository;
        private readonly IPollOptionService _pollOptionService;

        public PollOptionServiceTests()
        {
            _pollOptionRepository = Substitute.For<IPollOptionRepository>();
            _pollOptionService = new PollOptionService(_pollOptionRepository);
        }

        [Test]
        public void CreatePollOption_NullObject_ExpectsException()
        {
            _pollOptionRepository.Insert(null).Throws<NullReferenceException>();

            Assert.Throws<ArgumentNullException>(() => _pollOptionService.CreatePollOption(null));
            _pollOptionRepository.DidNotReceive().Insert(null);
        }

        [Test]
        public void CreatePollOption_WithoutRequiredFields_ExpectsException()
        {
            var pollOption = new PollOption(Guid.NewGuid(), new Poll());
            _pollOptionRepository.Insert(pollOption).Returns(pollOption);

            Assert.Throws<ArgumentException>(() => _pollOptionService.CreatePollOption(pollOption));
            _pollOptionRepository.DidNotReceive().Insert(pollOption);
        }

        [Test]
        public void CreatePollOption_WithRequiredFields_ReturnsPollOption()
        {
            var pollOption = new PollOption(Guid.Parse("313F6636-CA18-4CD4-AE68-27F1AE9833B2"), new Poll(Guid.Parse("C764F6A5-84CC-4CAB-ACE4-B2AF703196C2")), "Razocaran/a sam");
            _pollOptionRepository.Insert(pollOption).Returns(pollOption);

            var returnedPollOption = _pollOptionService.CreatePollOption(pollOption);
            _pollOptionRepository.Received(1).Insert(pollOption);
            Assert.AreEqual(pollOption, returnedPollOption);
        }

        [Test]
        public void UpdatePollOption_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _pollOptionService.UpdatePollOption(null));
            _pollOptionRepository.DidNotReceive().Update(null);
        }

        [Test]
        public void UpdatePollOption_WithoutRequiredFields_ExpectsException()
        {
            var pollOption = new PollOption();

            Assert.Throws<ArgumentException>(() => _pollOptionService.UpdatePollOption(pollOption));
            _pollOptionRepository.DidNotReceive().Update(pollOption);
        }

        [Test]
        public void UpdatePollOption_WithRequiredFields_ReturnsNothing()
        {
            var pollOption = new PollOption(Guid.Parse("313F6636-CA18-4CD4-AE68-27F1AE9833B2"), new Poll(Guid.Parse("C764F6A5-84CC-4CAB-ACE4-B2AF703196C2")), "Razocaran/a sam");
            _pollOptionService.UpdatePollOption(pollOption);
            _pollOptionRepository.Received(1).Update(pollOption);
        }

        [Test]
        public void DeletePollOption_WhichNotExists_ReturnsTrue()
        {
            var pollOptionId = Guid.NewGuid();
            var pollOptionTypeId = Guid.NewGuid();

            _pollOptionRepository.Delete(pollOptionId).Returns(false);
            Assert.False(_pollOptionService.DeletePollOption(pollOptionId));
        }

        [Test]
        public void DeletePollOption_WhichExists_ReturnsTrue()
        {
            var pollOptionId = Guid.Parse("F6AFCC3E-DD34-421B-8573-23695441F910");

            _pollOptionRepository.Delete(pollOptionId).Returns(true);
            Assert.True(_pollOptionService.DeletePollOption(pollOptionId));
        }

        [Test]
        public void GetPollOptionByPollId_WithoutData_ReturnsEmptyList()
        {
            var pollId = Guid.Parse("B953E5F6-2DE5-4B9C-B2C4-17E62DDAE850");
            _pollOptionRepository.Get(pollId)
                .Returns(new List<PollOption>());

            var returnedPollOptions = _pollOptionService.GetByPollId(pollId);
            Assert.AreEqual(0, returnedPollOptions.Count());
        }

        [Test]
        public void GetPollOptionByPollId_WithData_ReturnsPollOption()
        {
            var pollOptions = new List<PollOption>()
            {
                new PollOption(Guid.Parse("313F6636-CA18-4CD4-AE68-27F1AE9833B2"), new Poll(Guid.Parse("C764F6A5-84CC-4CAB-ACE4-B2AF703196C2")), "Razocaran/a sam"),
                new PollOption(Guid.Parse("3910806B-C9C9-429F-96AE-4C70568F7D0D"), new Poll(Guid.Parse("C764F6A5-84CC-4CAB-ACE4-B2AF703196C2")), "Zadovoljan/a sam"),
                new PollOption(Guid.Parse("E8F80DB5-722B-48C2-951E-74BC80FF6978"), new Poll(Guid.Parse("C764F6A5-84CC-4CAB-ACE4-B2AF703196C2")), "Više nego zadovoljan/a"),
                new PollOption(Guid.Parse("F12F569D-2E94-4B29-B121-AB3DBF3CB24A"), new Poll(Guid.Parse("C764F6A5-84CC-4CAB-ACE4-B2AF703196C2")), "Oduševljen/a")
            };
            var pollId = Guid.Parse("C764F6A5-84CC-4CAB-ACE4-B2AF703196C2");
            _pollOptionRepository.Get(pollId)
                .Returns(pollOptions.Where(po => po.Poll.PollId == pollId));

            var returnedPollOptions = _pollOptionService.GetByPollId(pollId);
            Assert.AreEqual(pollOptions.Where(po => po.Poll.PollId == pollId).Count(), returnedPollOptions.Count());
        }
    }
}
