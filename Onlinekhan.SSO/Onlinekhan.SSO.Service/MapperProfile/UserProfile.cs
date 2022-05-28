using AutoMapper;
using Onlinekhan.SSO.DomainClasses.Entities;
using Onlinekhan.SSO.ViewModels.User;

namespace Onlinekhan.SSO.ServiceLayer.MapperProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateViewModel, User>();
            //CreateMap<UserPreRegisterViewModel, User>();
            CreateMap<UserUpdateViewModel, User>();
            CreateMap<UserViewModel, User>()
                .ReverseMap();

        }
    }
}
