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
    public class PollRepositoryTests
    {
        private readonly IDbContext _dbContext;
        private readonly IPollRepository _pollRepository;

        public PollRepositoryTests()
        {
            _dbContext = new DbContext("Data Source=DESKTOP-883AG4N\\SQLEXPRESS;Initial Catalog=IpertechCompany;Integrated Security=True");
            _pollRepository = new PollRepository(_dbContext);
        }

        [Test]
        public void GetById_WithExistingId_ReturnsPoll()
        {
            Assert.AreEqual(0, _pollRepository.Get(Guid.Parse("BC8B7119-8C37-4034-9ABE-36E1334C86CE")).NumberOfVoters);
        }

        [Test]
        public void GetById_WithoutExistingId_ReturnsNull()
        {
            Assert.AreEqual(null, _pollRepository.Get(Guid.NewGuid()));
        }

        [Test]
        public void Insert_WithRequiredFields_ReturnsPoll()
        {
            var pollId = new Guid();
            var poll = new Poll(pollId, "Insert Question");

            _pollRepository.Insert(poll);
            Assert.AreEqual(poll.Question, _pollRepository.Get(poll.PollId).Question);
        }

        [Test]
        public void Insert_NullObject_ThrowsException()
        {
            Assert.Throws<NullReferenceException>(() => _pollRepository.Insert(null));
        }

        [Test]
        public void Update_WithRequiredFields_ReturnsPoll()
        {
            var pollId = Guid.Parse("BC8B7119-8C37-4034-9ABE-36E1334C86CE");
            var poll = new Poll(pollId, "Update Question");

            _pollRepository.Update(poll);
            var updatedPoll = _pollRepository
                .Get(poll.PollId);

            Assert.AreEqual(poll.Question, updatedPoll.Question);
        }

        [Test]
        public void Update_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _pollRepository.Update(null));
        }

        [Test]
        public void Delete_WhichExists_ReturnsTrue()
        {
            Assert.True(_pollRepository.Delete(Guid.Parse("C764F6A5-84CC-4CAB-ACE4-B2AF703196C2")));
        }

        [Test]
        public void Delete_WhichNotExists_ReturnsFalse()
        {
            Assert.False(_pollRepository.Delete(Guid.NewGuid()));
        }
    }
}
