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
            CreateMap<Notification, NotificationViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<UserLogin, UserLoginViewModel>().ReverseMap();
            CreateMap<InternetRouter, InternetRouterViewModel>().ReverseMap();
            CreateMap<InternetPacket, InternetPacketViewModel>().ReverseMap();
            CreateMap<PhonePacket, PhonePacketViewModel>().ReverseMap();
            CreateMap<TvChannel, TvChannelViewModel>().ReverseMap();
            CreateMap<TvPacket, TvPacketViewModel>().ReverseMap();
            CreateMap<TvPacketTvChannel, TvPacketTvChannelViewModel>().ReverseMap();
            CreateMap<PacketCombination, PacketCombinationViewModel>().ReverseMap();
            CreateMap<UserContract, UserContractViewModel>().ReverseMap();
            CreateMap<Message, MessageViewModel>().ReverseMap();
            CreateMap<UserMessage, UserMessageViewModel>().ReverseMap();
            CreateMap<Bill, BillViewModel>().ReverseMap();
            CreateMap<Poll, PollViewModel>().ReverseMap();
            CreateMap<PollOption, PollOptionViewModel>().ReverseMap();
        }
    }
}
