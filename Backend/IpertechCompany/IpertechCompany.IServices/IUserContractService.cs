using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IServices
{
    public interface IUserContractService
    {
        IEnumerable<UserContract> GetAllUserContracts();
        UserContract CreateUserContract(UserContract userContract);
        void UpdateUserContract(UserContract userContract);
        bool DeleteUserContract(Guid userContractId);
    }
}
