using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

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
