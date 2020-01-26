using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IRepositories
{
    interface IInternetRouterRepository
    {
        InternetRouter Get(Guid internetRouterId);
        InternetRouter Insert(InternetRouter internetRouter);
        void Update(InternetRouter internetRouter);
        bool Delete(Guid internetRouterId);
    }
}
