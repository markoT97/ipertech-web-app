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
    public class BillServiceTests
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillService _billService;

        public BillServiceTests()
        {
            _billRepository = Substitute.For<IBillRepository>();
            _billService = new BillService(_billRepository);
        }

        [Test]
        public void CreateBill_NullObject_ExpectsException()
        {
            _billRepository.Insert(null).Throws<NullReferenceException>();

            Assert.Throws<ArgumentNullException>(() => _billService.CreateBill(null));
            _billRepository.DidNotReceive().Insert(null);
        }

        [Test]
        public void CreateBill_WithoutRequiredFields_ExpectsException()
        {
            var bill = new Bill(Guid.NewGuid(), new UserContract());
            _billRepository.Insert(bill).Returns(bill);

            Assert.Throws<ArgumentException>(() => _billService.CreateBill(bill));
            _billRepository.DidNotReceive().Insert(bill);
        }

        [Test]
        public void CreateBill_WithRequiredFields_ReturnsBill()
        {
            var bill = new Bill(Guid.NewGuid(), new UserContract(Guid.NewGuid()), DateTime.UtcNow, DateTime.Now.AddDays(30), "434324234423", "4324324", false, 10,
                "RSD");
            _billRepository.Insert(bill).Returns(bill);

            var returnedBill = _billService.CreateBill(bill);
            _billRepository.Received(1).Insert(bill);
            Assert.AreEqual(bill, returnedBill);
        }

        [Test]
        public void UpdateBill_NullObject_ExpectsException()
        {
            Assert.Throws<ArgumentNullException>(() => _billService.UpdateBill(null));
            _billRepository.DidNotReceive().Update(null);
        }

        [Test]
        public void UpdateBill_WithoutRequiredFields_ExpectsException()
        {
            var bill = new Bill();

            Assert.Throws<ArgumentException>(() => _billService.UpdateBill(bill));
            _billRepository.DidNotReceive().Update(bill);
        }

        [Test]
        public void UpdateBill_WithRequiredFields_ReturnsNothing()
        {
            var bill = new Bill(Guid.Parse("4F6327C6-E149-4F86-BD09-426BFF1178B2"),
                new UserContract(Guid.Parse("2E97AAA3-E364-4C32-BC4C-1895FF066492")), DateTime.UtcNow, DateTime.Now.AddDays(30), "00", "1111", false, 1200,
                "RSD");
            _billService.UpdateBill(bill);
            _billRepository.Received(1).Update(bill);
        }

        [Test]
        public void DeleteBill_WhichNotExists_ReturnsTrue()
        {
            var billId = Guid.NewGuid();

            _billRepository.Delete(billId).Returns(false);
            Assert.False(_billService.DeleteBill(billId));
        }

        [Test]
        public void DeleteBill_WhichExists_ReturnsTrue()
        {
            var billId = Guid.Parse("4F6327C6-E149-4F86-BD09-426BFF1178B2");

            _billRepository.Delete(billId).Returns(true);
            Assert.True(_billService.DeleteBill(billId));
        }

        [Test]
        public void GetBillByUserContractId_WithoutData_ReturnsEmptyList()
        {
            var userContractId = Guid.Parse("2E97AAA3-E364-4C32-BC4C-1895FF066492");
            _billRepository.Get(userContractId, 0, 10)
                .Returns(new List<Bill>());

            var returnedBills = _billService.GetByUserContractId(userContractId, 0, 10);
            Assert.AreEqual(0, returnedBills.Count());
        }

        [Test]
        public void GetBillByUserContractId_WithData_ReturnsOneBill()
        {
            var bills = new List<Bill>()
            {
                new Bill(Guid.Parse("4F6327C6-E149-4F86-BD09-426BFF1178B2"),
                    new UserContract(Guid.Parse("2E97AAA3-E364-4C32-BC4C-1895FF066492")), DateTime.UtcNow, DateTime.Now.AddDays(30), "00", "1111", false, 1200,
                    "RSD"),
                new Bill(Guid.Parse("B78FDB5A-FD24-49C2-B89D-CEC701EB5DC8"),
                    new UserContract(Guid.Parse("E352D17F-E719-40F1-B1AE-660510F3DBC4")), DateTime.UtcNow, DateTime.Now.AddDays(30), "00", "1111", false, 1345,
                    "RSD"),
                new Bill(Guid.Parse("D0BE13C7-F54F-46AD-AC46-FB65329542A4"),
                    new UserContract(Guid.Parse("BAA27786-50D0-4568-95FA-67703D38BEAB")), DateTime.UtcNow, DateTime.Now.AddDays(30), "00", "1111", false, 2245,
                    "RSD")
            };
            var userContractId = Guid.Parse("2E97AAA3-E364-4C32-BC4C-1895FF066492");
            _billRepository.Get(userContractId, 0, 10)
                .Returns(bills.Where(b => b.UserContract.UserContractId == userContractId));

            var returnedBills = _billService.GetByUserContractId(userContractId, 0, 10);
            Assert.AreEqual(bills.Count(b => b.UserContract.UserContractId == userContractId), returnedBills.Count());
        }
    }
}
