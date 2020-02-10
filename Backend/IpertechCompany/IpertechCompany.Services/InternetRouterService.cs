using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using System;

namespace IpertechCompany.Services
{
    public class InternetRouterService : IInternetRouterService
    {
        private readonly IInternetRouterRepository _internetRouterRepository;

        public InternetRouterService(IInternetRouterRepository internetRouterRepository)
        {
            _internetRouterRepository = internetRouterRepository;
        }

        public InternetRouter CreateInternetRouter(InternetRouter internetRouter)
        {
            if (!(internetRouter != null))
            {
                throw new ArgumentNullException("internetRouter", "Parameter is null.");
            }

            if (!internetRouter.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            return _internetRouterRepository.Insert(internetRouter);
        }

        public bool DeleteInternetRouter(Guid internetRouterId)
        {
            return _internetRouterRepository.Delete(internetRouterId);
        }

        public InternetRouter GetByInternetRouterId(Guid internetRouterId)
        {
            return _internetRouterRepository.Get(internetRouterId);
        }

        public void UpdateInternetRouter(InternetRouter internetRouter)
        {
            if (!(internetRouter != null))
            {
                throw new ArgumentNullException("internetRouter", "Parameter is null.");
            }

            if (!internetRouter.IsValid())
            {
                throw new ArgumentException("Missing required properties.");
            }

            _internetRouterRepository.Update(internetRouter);
        }
    }
}
