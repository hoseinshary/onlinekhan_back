using AutoMapper;
using Onlinekhan.SSO.DomainClasses.Entities;
using Onlinekhan.SSO.ViewModels.Role;

namespace Onlinekhan.SSO.ServiceLayer.MapperProfile
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
