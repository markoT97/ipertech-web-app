﻿using IpertechCompany.Models;
using System;
using System.Collections.Generic;

namespace IpertechCompany.IRepositories
{
    public interface IUserMessageRepository
    {
        IEnumerable<Message> Get(Guid userId, int offset, int numberOfRows);
        int Get(Guid userId);
        UserMessage Insert(UserMessage userMessage);
        bool Delete(UserMessage userMessage);
    }
}
