using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IRepositories
{
    public interface IBillRepository
    {
        IEnumerable<Bill> Get(Guid userContractId, int offset, int numberOfRows);
        int Get(Guid userContractId);
        Bill Insert(Bill bill);
        void Update(Bill bill);
        bool Delete(Guid billId);
    }
}
