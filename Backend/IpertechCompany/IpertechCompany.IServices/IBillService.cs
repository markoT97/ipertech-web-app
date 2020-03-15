﻿using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IServices
{
    public interface IBillService
    {
        IEnumerable<Bill> GetByUserContractId(Guid userContractId, int offset, int numberOfRows);
        int GetTotalNumberOfBillsByUserContractId(Guid userContractId);
        Bill CreateBill(Bill bill);
        void UpdateBill(Bill bill);
        bool DeleteBill(Guid billId);
    }
}
