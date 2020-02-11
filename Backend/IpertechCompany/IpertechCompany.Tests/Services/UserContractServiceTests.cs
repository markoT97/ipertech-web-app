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
    public class UserContractServiceTests
    {
        private readonly IUserContractRepository _userContractRepository;
        private readonly IUserContractService _userContractService;

        public UserContractServiceTests()
        {
            _userContractRepository = Substitute.For<IUserContractRepository>();
            _userContractService = new UserContractService(_userContractRepository);
        }

        [Test]
        public void CreateUserContract_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _userContractService.CreateUserContract(null));
            _userContractRepository.DidNotReceive().Insert(null);
        }

        [Test]
        public void CreateUserContract_WithoutRequitedFields_ExpectsException()
        {
            var userContract = new UserContract();

            Assert.Throws<ArgumentException>(() => _userContractService.CreateUserContract(userContract));
            _userContractRepository.DidNotReceive().Insert(userContract);
        }

        [Test]
        public void CreateUserContract_WithRequiredFields_ReturnsUserContract()
        {
            var userContract = new UserContract(Guid.Parse("082C90F1-F513-4B2D-ACF1-14B277D6D6C8"), new PacketCombination(Guid.Parse("1B25A58C-24D9-4B3A-8518-E9201B70E6A9")), 12);
            _userContractRepository.Insert(userContract).Returns(userContract);

            var returnedUserContract = _userContractService.CreateUserContract(userContract);
            _userContractRepository.Received(1).Insert(userContract);
            Assert.AreEqual(userContract, returnedUserContract);
        }

        [Test]
        public void UpdateUserContract_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _userContractService.UpdateUserContract(null));
            _userContractRepository.DidNotReceive().Update(null);
        }

        [Test]
        public void UpdateUserContract_WithoutRequiredFields_ExpectsException()
        {
            var userContract = new UserContract();

            Assert.Throws<ArgumentException>(() => _userContractService.UpdateUserContract(userContract));
            _userContractRepository.DidNotReceive().Update(userContract);
        }

        [Test]
        public void UpdateUserContract_WithRequiredFields_ReturnsNothing()
        {
            var userContract = new UserContract(Guid.Parse("082C90F1-F513-4B2D-ACF1-14B277D6D6C8"), new PacketCombination(Guid.Parse("1B25A58C-24D9-4B3A-8518-E9201B70E6A9")), 12);
            _userContractService.UpdateUserContract(userContract);
            _userContractRepository.Received(1).Update(userContract);
        }

        [Test]
        public void GetAllUserContracts_WithoutData_ReturnsEmptyList()
        {
            _userContractRepository.GetAll().Returns(new List<UserContract>());
            Assert.AreEqual(0, _userContractService.GetAllUserContracts().Count());
        }

        [Test]
        public void GetAllUserContracts_WithData_ReturnsPopulatedList()
        {
            var userContracts = new List<UserContract>()
            {
                new UserContract(Guid.Parse("082C90F1-F513-4B2D-ACF1-14B277D6D6C8"), new PacketCombination(Guid.Parse("1B25A58C-24D9-4B3A-8518-E9201B70E6A9")), 12),
                new UserContract(Guid.Parse("2E97AAA3-E364-4C32-BC4C-1895FF066492"), new PacketCombination(Guid.Parse("78B15548-9DFA-4984-ACC8-70A5B801CA6D")), 24),
                new UserContract(Guid.Parse("E352D17F-E719-40F1-B1AE-660510F3DBC4"), new PacketCombination(Guid.Parse("2D03F2E9-0E5B-4562-AC71-C08755188AD1")), 12),
                new UserContract(Guid.Parse("BAA27786-50D0-4568-95FA-67703D38BEAB"), new PacketCombination(Guid.Parse("1B25A58C-24D9-4B3A-8518-E9201B70E6A9")), 24)
            };
            _userContractRepository.GetAll().Returns(userContracts);

            Assert.AreEqual(userContracts.Count, _userContractService.GetAllUserContracts().Count());
        }
    }
}
