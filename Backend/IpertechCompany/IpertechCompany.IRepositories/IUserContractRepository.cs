using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IRepositories
{
    interface IUserContractRepository
    {
        IEnumerable<UserContract> GetAll();
        UserContract Insert(UserContract userContract);
        void Update(UserContract userContract);
        bool Delete(Guid userContractId);
    }
}
