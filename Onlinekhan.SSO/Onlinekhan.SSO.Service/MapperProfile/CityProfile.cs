using AutoMapper;
using Onlinekhan.SSO.DomainClasses.Entities;
using Onlinekhan.SSO.ViewModels.City;

namespace Onlinekhan.SSO.ServiceLayer.MapperProfile
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<CityViewModel, City>();
            CreateMap<CityCreateViewModel, City>();
            CreateMap<CityUpdateViewModel, City>();
        }
    }
}
