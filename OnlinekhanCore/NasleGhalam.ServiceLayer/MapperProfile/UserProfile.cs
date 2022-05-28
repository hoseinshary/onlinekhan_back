using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateViewModel, User>();
            CreateMap<UserPreRegisterViewModel, User>();
            CreateMap<UserUpdateViewModel, User>();
            CreateMap<UserViewModel, User>()
                .ReverseMap();

        }
    }
}
