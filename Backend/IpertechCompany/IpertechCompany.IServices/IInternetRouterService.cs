using IpertechCompany.Models;
using System;

namespace IpertechCompany.IServices
{
    public interface IInternetRouterService
    {
        InternetRouter GetByInternetRouterId(Guid internetRouterId);
        InternetRouter CreateInternetRouter(InternetRouter internetRouter);
        void UpdateInternetRouter(InternetRouter internetRouter);
        bool DeleteInternetRouter(Guid internetRouterId);
    }
}
