using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.Services
{
    public class UserContractService : IUserContractService
    {
        private readonly IUserContractRepository _userContractRepository;

        public UserContractService(IUserContractRepository userContract)
        {
            _userContractRepository = userContract;
        }

        public UserContract CreateUserContract(UserContract userContract)
        {
            if (!(userContract != null))
            {
                throw new ArgumentNullException("userContract", "Parameter is null.");
            }

            if (!userContract.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            return _userContractRepository.Insert(userContract);
        }

        public bool DeleteUserContract(Guid userContractId)
        {
            return _userContractRepository.Delete(userContractId);
        }

        public IEnumerable<UserContract> GetAllUserContracts()
        {
            return _userContractRepository.GetAll();
        }

        public UserContract GetByUserContractId(Guid userContractId)
        {
            return _userContractRepository.GetById(userContractId);
        }

        public void UpdateUserContract(UserContract userContract)
        {
            if (!(userContract != null))
            {
                throw new ArgumentNullException("userContract", "Parameter is null.");
            }

            if (!userContract.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }
            _userContractRepository.Update(userContract);
        }
    }
}
