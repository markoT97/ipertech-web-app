using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IRepositories
{
    public interface IPollOptionRepository
    {
        IEnumerable<PollOption> Get(Guid pollId);
        PollOption Insert(PollOption pollOption);
        void Update(PollOption pollOption);
        bool Delete(Guid pollOptionId);
    }
}
