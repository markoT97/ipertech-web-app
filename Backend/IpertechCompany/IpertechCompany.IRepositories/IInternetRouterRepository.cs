using IpertechCompany.Models;
using System;

namespace IpertechCompany.IRepositories
{
    public interface IInternetRouterRepository
    {
        InternetRouter Get(Guid internetRouterId);
        InternetRouter Insert(InternetRouter internetRouter);
        void Update(InternetRouter internetRouter);
        bool Delete(Guid internetRouterId);
    }
}
