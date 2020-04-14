﻿using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IRepositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(Guid userContractId);
        User Get(UserLogin userLogin);
        User Insert(User user);
        void Update(User user);
        void Update(UserImage userImage);
        void Update(UserPassword userPassword);
        bool Delete(Guid userId);
    }
}
