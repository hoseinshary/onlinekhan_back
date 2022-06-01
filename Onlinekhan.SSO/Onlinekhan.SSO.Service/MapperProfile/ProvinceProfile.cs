using AutoMapper;
using Onlinekhan.SSO.DomainClasses.Entities;
using Onlinekhan.SSO.ViewModels.Province;

namespace Onlinekhan.SSO.ServiceLayer.MapperProfile
{
    public class ProvinceProfile : Profile
    {
        public ProvinceProfile()
        {
            CreateMap<ProvinceViewModel, Province>();
        }
    }
}
