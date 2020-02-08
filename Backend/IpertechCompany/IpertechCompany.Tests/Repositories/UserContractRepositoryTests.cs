using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IpertechCompany.DbConnection;
using IpertechCompany.DbRepositories;
using IpertechCompany.IRepositories;
using IpertechCompany.Models;
using NUnit.Framework;

namespace IpertechCompany.Tests.Repositories
{
    [TestFixture]
    public class UserContractRepositoryTests
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContractRepository _userContractRepository;

        public UserContractRepositoryTests()
        {
            _dbContext = new DbContext("Data Source=DESKTOP-883AG4N\\SQLEXPRESS;Initial Catalog=IpertechCompany;Integrated Security=True");
            _userContractRepository = new UserContractRepository(_dbContext);
        }
        

        [Test]
        public void GetAll_WithData_ReturnsListOfThreeUserContracts()
        {
            Assert.AreEqual(3, _userContractRepository.GetAll().Count());
        }

        [Test]
        public void Insert_WithRequiredFields_ReturnsListWithFourUserContracts()
        {
            var userContract = new UserContract(Guid.NewGuid(), new PacketCombination(Guid.Parse("78B15548-9DFA-4984-ACC8-70A5B801CA6D")), 24);

            _userContractRepository.Insert(userContract);
            Assert.AreEqual(4, _userContractRepository.GetAll().Count());
        }

        [Test]
        public void Insert_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _userContractRepository.Insert(null));
        }

        [Test]
        public void Update_WithRequiredFields_ReturnsUserContract()
        {
            var userContract = new UserContract(Guid.Parse("E352D17F-E719-40F1-B1AE-660510F3DBC4"),
                new PacketCombination(Guid.Parse("78B15548-9DFA-4984-ACC8-70A5B801CA6D")),
                24);

            _userContractRepository.Update(userContract);
            var updatedUserContract = _userContractRepository
                .GetAll().Single(pc => pc.UserContractId == userContract.UserContractId);

            Assert.AreEqual(userContract.ContractDurationMonths, updatedUserContract.ContractDurationMonths);
        }

        [Test]
        public void Update_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _userContractRepository.Update(null));
        }

        [Test]
        public void Delete_WhichExists_ReturnsTrue()
        {
            Assert.True(_userContractRepository.Delete(Guid.Parse("BAA27786-50D0-4568-95FA-67703D38BEAB")));
        }

        [Test]
        public void Delete_WhichNotExists_ReturnsFalse()
        {
            Assert.False(_userContractRepository.Delete(Guid.NewGuid()));
        }
    }
}
