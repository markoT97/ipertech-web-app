using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IRepositories
{
    public interface IUserContractRepository
    {
        IEnumerable<UserContract> GetAll();
        UserContract Insert(UserContract userContract);
        void Update(UserContract userContract);
        bool Delete(Guid userContractId);
    }
}
