﻿using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IRepositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(Guid userId);
        User Get(UserLogin userLogin);
        User Insert(User user);
        void Update(User user);
        void Update(UserImage userImage);
        bool Delete(Guid userId);
    }
}
