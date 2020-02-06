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
    public class PollOptionRepositoryTests
    {
        private readonly IDbContext _dbContext;
        private readonly IPollOptionRepository _pollOptionRepository;

        public PollOptionRepositoryTests()
        {
            _dbContext = new DbContext("Data Source=DESKTOP-883AG4N\\SQLEXPRESS;Initial Catalog=IpertechCompany;Integrated Security=True");
            _pollOptionRepository = new PollOptionRepository(_dbContext);
        }

        [Test]
        public void GetById_WithExistingId_ReturnsPollOption()
        {
            Assert.AreEqual(3, _pollOptionRepository.Get(Guid.Parse("C764F6A5-84CC-4CAB-ACE4-B2AF703196C2")).Count());
        }

        [Test]
        public void GetById_WithoutExistingId_ReturnsEmptyList()
        {
            Assert.AreEqual(0, _pollOptionRepository.Get(Guid.NewGuid()).Count());
        }

        [Test]
        public void Insert_WithRequiredFields_ReturnsPollOption()
        {
            var pollOptionId = new Guid();
            var pollOption = new PollOption(pollOptionId, new Poll(Guid.Parse("C764F6A5-84CC-4CAB-ACE4-B2AF703196C2")), "Insert Answer");

            _pollOptionRepository.Insert(pollOption);
            Assert.AreEqual(4, _pollOptionRepository.Get(pollOption.Poll.PollId).Count());
        }

        [Test]
        public void Insert_NullObject_ThrowsException()
        {
            Assert.Throws<NullReferenceException>(() => _pollOptionRepository.Insert(null));
        }

        [Test]
        public void Update_WithRequiredFields_ReturnsPollOption()
        {
            var pollOptionId = Guid.Parse("3910806B-C9C9-429F-96AE-4C70568F7D0D");
            var pollOption = new PollOption(pollOptionId, new Poll(Guid.Parse("C764F6A5-84CC-4CAB-ACE4-B2AF703196C2")), "Update Answer");

            _pollOptionRepository.Update(pollOption);
            var updatedPollOption = _pollOptionRepository
                .Get(pollOption.Poll.PollId).Single(po => po.PollOptionId.Equals(pollOptionId));

            Assert.AreEqual(pollOption.AnswerText, updatedPollOption.AnswerText);
        }

        [Test]
        public void Update_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _pollOptionRepository.Update(null));
        }

        [Test]
        public void Delete_WhichExists_ReturnsTrue()
        {
            Assert.True(_pollOptionRepository.Delete(Guid.Parse("F12F569D-2E94-4B29-B121-AB3DBF3CB24A")));
        }

        [Test]
        public void Delete_WhichNotExists_ReturnsFalse()
        {
            Assert.False(_pollOptionRepository.Delete(Guid.NewGuid()));
        }
    }
}
