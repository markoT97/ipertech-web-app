using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.DbConnection;
using IpertechCompany.DbRepositories;
using IpertechCompany.IRepositories;
using IpertechCompany.Models;
using NUnit.Framework;

namespace IpertechCompany.Tests.Repositories
{
    [TestFixture]
    public class OptionVoterRepositoryTests
    {
        private readonly IDbContext _dbContext;
        private readonly IOptionVoterRepository _optionVoterRepository;

        public OptionVoterRepositoryTests()
        {
            _dbContext = new DbContext("Data Source=DESKTOP-883AG4N\\SQLEXPRESS;Initial Catalog=IpertechCompany;Integrated Security=True");
            _optionVoterRepository = new OptionVoterRepository(_dbContext);
        }

        [Test]
        public void GetById_WithExistingId_ReturnsOneVoter()
        {
            Assert.AreEqual(1, _optionVoterRepository.Get(Guid.Parse("F12F569D-2E94-4B29-B121-AB3DBF3CB24A")));
        }
        
        [Test]
        public void GetById_WithoutExistingId_ReturnsNull()
        {
            Assert.AreEqual(0, _optionVoterRepository.Get(Guid.NewGuid()));
        }

        [Test]
        public void Insert_WithRequiredFields_ReturnsOptionVoter()
        {
            var optionVoter = new OptionVoter(Guid.Parse("8F10E3A8-5B61-42F0-B00A-243F0FA2D228"), Guid.Parse("C764F6A5-84CC-4CAB-ACE4-B2AF703196C2"), Guid.Parse("F12F569D-2E94-4B29-B121-AB3DBF3CB24A"));

            _optionVoterRepository.Insert(optionVoter);
            Assert.AreEqual(2, _optionVoterRepository.Get(optionVoter.PollOptionId));
        }

        [Test]
        public void Insert_NullObject_ThrowsException()
        {
            Assert.Throws<NullReferenceException>(() => _optionVoterRepository.Insert(null));
        }
    }
}
