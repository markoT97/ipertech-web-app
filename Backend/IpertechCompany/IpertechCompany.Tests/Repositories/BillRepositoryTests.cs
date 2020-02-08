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
    public class BillRepositoryTests
    {
        [TestFixture]
        public class PacketCombinationRepositoryTests
        {
            private readonly IDbContext _dbContext;
            private readonly IBillRepository _billRepository;

            public PacketCombinationRepositoryTests()
            {
                _dbContext =
                    new DbContext(
                        "Data Source=DESKTOP-883AG4N\\SQLEXPRESS;Initial Catalog=IpertechCompany;Integrated Security=True");
                _billRepository = new BillRepository(_dbContext);
            }


            [Test]
            public void GetByUserContractId_WithExistingUserContractId_ReturnsPopulatedList()
            {
                Assert.AreEqual(1, _billRepository.Get(Guid.Parse("2E97AAA3-E364-4C32-BC4C-1895FF066492")).Count());
            }

            [Test]
            public void GetByUserContractId_WithoutExistingUserContractId_ReturnsEmptyList()
            {
                Assert.AreEqual(0, _billRepository.Get(Guid.NewGuid()).Count());
            }

            [Test]
            public void Insert_WithRequiredFields_ReturnsListWithFourBills()
            {
                var bill = new Bill(Guid.Parse("061E89AA-59AB-4F9F-B658-47088C14786A"), new UserContract(Guid.Parse("2E97AAA3-E364-4C32-BC4C-1895FF066492")), "Test Insert Call", "Test Insert Acc", false, 0, "RSD");

                _billRepository.Insert(bill);
                Assert.AreEqual(2, _billRepository.Get(Guid.Parse("2E97AAA3-E364-4C32-BC4C-1895FF066492")).Count());
            }

            [Test]
            public void Insert_NullObject_ExpectsException()
            {
                Assert.Throws<NullReferenceException>(() => _billRepository.Insert(null));
            }

            [Test]
            public void Update_WithRequiredFields_ReturnsBill()
            {
                var bill = new Bill(Guid.Parse("4F6327C6-E149-4F86-BD09-426BFF1178B2"),
                    new UserContract(Guid.Parse("2E97AAA3-E364-4C32-BC4C-1895FF066492")),
                    "Update Call",
                    "Update Acc",
                    false,
                    0,
                    "RSD");

                _billRepository.Update(bill);
                var updatedBill = _billRepository
                    .Get(Guid.Parse("2E97AAA3-E364-4C32-BC4C-1895FF066492")).First();

                Assert.AreEqual(bill.CallNum, updatedBill.CallNum);
            }

            [Test]
            public void Update_NullObject_ExpectsException()
            {
                Assert.Throws<NullReferenceException>(() => _billRepository.Update(null));
            }

            [Test]
            public void Delete_WhichExists_ReturnsTrue()
            {
                Assert.True(_billRepository.Delete(Guid.Parse("B78FDB5A-FD24-49C2-B89D-CEC701EB5DC8")));
            }

            [Test]
            public void Delete_WhichNotExists_ReturnsFalse()
            {
                Assert.False(_billRepository.Delete(Guid.NewGuid()));
            }
        }
    }
}
