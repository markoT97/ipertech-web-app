using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IRepositories
{
    interface IBillRepository
    {
        IEnumerable<Bill> Get(Guid userContractId);
        Bill Insert(Bill bill);
        void Update(Bill bill);
        bool Delete(Guid billId);
    }
}
