using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Role;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleViewModel>().ReverseMap();
            CreateMap<RoleCreateViewModel, Role>();
            CreateMap<RoleUpdateViewModel, Role>();
        }
    }
}
