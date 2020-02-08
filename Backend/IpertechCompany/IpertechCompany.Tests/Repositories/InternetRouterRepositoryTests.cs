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
    public class InternetRouterRepositoryTests
    {
        private readonly IDbContext _dbContext;
        private readonly IInternetRouterRepository _internetRouterRepository;

        public InternetRouterRepositoryTests()
        {
            _dbContext = new DbContext("Data Source=DESKTOP-883AG4N\\SQLEXPRESS;Initial Catalog=IpertechCompany;Integrated Security=True");
            _internetRouterRepository = new InternetRouterRepository(_dbContext);
        }

        [Test]
        public void GetById_WithExistingId_ReturnsInternetRouter()
        {
            Assert.AreEqual("Asus RT-AC66U B1 Dual-Band Gigabit Wi-Fi Router", _internetRouterRepository.Get(Guid.Parse("59659676-8043-49FC-804D-1621650838C7")).Name);
        }

        [Test]
        public void GetById_WithoutExistingId_ReturnsNull()
        {
            Assert.AreEqual(null, _internetRouterRepository.Get(Guid.NewGuid()));
        }

        [Test]
        public void Insert_WithRequiredFields_ReturnsInternetRouter()
        {
            var internetRouter = new InternetRouter(new Guid(), "Insert");

            _internetRouterRepository.Insert(internetRouter);
            Assert.AreEqual(internetRouter.Name, _internetRouterRepository.Get(internetRouter.InternetRouterId).Name);
        }

        [Test]
        public void Insert_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _internetRouterRepository.Insert(null));
        }

        [Test]
        public void Update_WithRequiredFields_ReturnsInternetRouter()
        {
            var internetRouter = new InternetRouter(Guid.Parse("59659676-8043-49FC-804D-1621650838C7"), "Update");

            _internetRouterRepository.Update(internetRouter);
            var updatedInternetRouter = _internetRouterRepository
                .Get(internetRouter.InternetRouterId);

            Assert.AreEqual(internetRouter.Name, updatedInternetRouter.Name);
        }

        [Test]
        public void Update_NullObject_ExpectsException()
        {
            Assert.Throws<NullReferenceException>(() => _internetRouterRepository.Update(null));
        }

        [Test]
        public void Delete_WhichExists_ReturnsTrue()
        {
            Assert.True(_internetRouterRepository.Delete(Guid.Parse("62359BE4-1124-4AE5-AC1B-72928E2354E8")));
        }

        [Test]
        public void Delete_WhichNotExists_ReturnsFalse()
        {
            Assert.False(_internetRouterRepository.Delete(Guid.NewGuid()));
        }
    }
}

