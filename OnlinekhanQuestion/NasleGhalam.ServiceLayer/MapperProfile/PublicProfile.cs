using AutoMapper;
using NasleGhalam.Common;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class PublicProfile : Profile
    {
        public PublicProfile()
        {
            CreateMap<ServerMessageResult, ClientMessageResult>()
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.FaMessage))
                .ForMember(dest => dest.Obj, opt => opt.Ignore());
        }
    }
}
