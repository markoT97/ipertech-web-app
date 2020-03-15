using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.Services
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _billRepository;

        public BillService(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        public Bill CreateBill(Bill bill)
        {
            if (!(bill != null))
            {
                throw new ArgumentNullException("bill", "Parameter is null.");
            }
            if (!bill.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            return _billRepository.Insert(bill);
        }

        public bool DeleteBill(Guid billId)
        {
            return _billRepository.Delete(billId);
        }

        public IEnumerable<Bill> GetByUserContractId(Guid userContractId, int offset, int numberOfRows)
        {
            if (!(userContractId != null))
            {
                throw new ArgumentException("Missing required properties.");
            }
            return _billRepository.Get(userContractId, offset, numberOfRows);
        }

        public int GetTotalNumberOfBillsByUserContractId(Guid userContractId)
        {
            if (!(userContractId != null))
            {
                throw new ArgumentException("Missing required properties.");
            }
            return _billRepository.Get(userContractId);
        }

        public void UpdateBill(Bill bill)
        {
            if (!(bill != null))
            {
                throw new ArgumentNullException("bill", "Parameter is null.");
            }
            if (!bill.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            _billRepository.Update(bill);
        }
    }
}
