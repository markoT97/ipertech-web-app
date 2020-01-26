using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IRepositories
{
    interface IPollRepository
    {
        Poll Get(Guid pollId);
        Poll Insert(Poll poll);
        void Update(Poll poll);
        bool Delete(Guid pollId);
    }
}
