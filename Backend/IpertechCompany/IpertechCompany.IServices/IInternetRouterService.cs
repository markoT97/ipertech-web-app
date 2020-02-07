using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

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
