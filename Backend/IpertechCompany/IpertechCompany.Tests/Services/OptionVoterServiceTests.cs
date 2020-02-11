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
    public class OptionVoterServiceTests
    {
        private readonly IOptionVoterRepository _optionVoterRepository;
        private readonly IOptionVoterService _optionVoterService;

        public OptionVoterServiceTests()
        {
            _optionVoterRepository = Substitute.For<IOptionVoterRepository>();
            _optionVoterService = new OptionVoterService(_optionVoterRepository);
        }

        [Test]
        public void CreateOptionVoter_NullObject_ExpectsException()
        {
            _optionVoterRepository.Insert(null).Throws<NullReferenceException>();

            Assert.Throws<ArgumentNullException>(() => _optionVoterService.CreateOptionVoter(null));
            _optionVoterRepository.DidNotReceive().Insert(null);
        }

        [Test]
        public void CreateOptionVoter_WithoutRequiredFields_ExpectsException()
        {
            var optionVoter = new OptionVoter(Guid.NewGuid());
            _optionVoterRepository.Insert(optionVoter).Returns(optionVoter);

            Assert.Throws<ArgumentException>(() => _optionVoterService.CreateOptionVoter(optionVoter));
            _optionVoterRepository.DidNotReceive().Insert(optionVoter);
        }

        [Test]
        public void CreateOptionVoter_WithRequiredFields_ReturnsOptionVoter()
        {
            var optionVoter = new OptionVoter(Guid.Parse("8D613A6B-AEF0-4B15-95F4-3BB5039F47DE"), Guid.Parse("C764F6A5-84CC-4CAB-ACE4-B2AF703196C2"), Guid.Parse("F12F569D-2E94-4B29-B121-AB3DBF3CB24A"));
            _optionVoterRepository.Insert(optionVoter).Returns(optionVoter);

            var returnedOptionVoter = _optionVoterService.CreateOptionVoter(optionVoter);
            _optionVoterRepository.Received(1).Insert(optionVoter);
            Assert.AreEqual(optionVoter, returnedOptionVoter);
        }

        [Test]
        public void GetNumberOfVotersByPollOptionId_WithoutData_ReturnsZero()
        {
            var pollOptionId = Guid.Parse("F12F569D-2E94-4B29-B121-AB3DBF3CB24A");
            _optionVoterRepository.Get(pollOptionId)
                .Returns(0);

            var returnedNumberOfVoters = _optionVoterService.GetNumberOfVotersByPollOptionId(pollOptionId);
            Assert.AreEqual(0, returnedNumberOfVoters);
        }

        [Test]
        public void GetNumberOfVotersByPollOptionId_WithData_ReturnsOptionVoter()
        {
            var optionVoters = new List<OptionVoter>()
            {
                new OptionVoter(Guid.Parse("8D613A6B-AEF0-4B15-95F4-3BB5039F47DE"), Guid.Parse("C764F6A5-84CC-4CAB-ACE4-B2AF703196C2"), Guid.Parse("F12F569D-2E94-4B29-B121-AB3DBF3CB24A")),
                new OptionVoter(Guid.Parse("8B1B2474-6A74-4EBB-A27B-86F6D6939266"), Guid.Parse("C764F6A5-84CC-4CAB-ACE4-B2AF703196C2"), Guid.Parse("3910806B-C9C9-429F-96AE-4C70568F7D0D"))
            };
            var pollOptionId = Guid.Parse("F12F569D-2E94-4B29-B121-AB3DBF3CB24A");
            _optionVoterRepository.Get(pollOptionId)
                .Returns(optionVoters.Count(ov => ov.PollOptionId == pollOptionId));

            var returnedNumberOfVoters = _optionVoterService.GetNumberOfVotersByPollOptionId(pollOptionId);
            Assert.AreEqual(optionVoters.Count(ov => ov.PollOptionId == pollOptionId), returnedNumberOfVoters);
        }

    }
}
