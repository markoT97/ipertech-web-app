using AutoMapper;
using IpertechCompany.Models;
using IpertechCompany.WebAPI.Models;

namespace IpertechCompany.WebAPI.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<NotificationType, NotificationTypeViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<UserLogin, UserLoginViewModel>().ReverseMap();
        }
    }
}
