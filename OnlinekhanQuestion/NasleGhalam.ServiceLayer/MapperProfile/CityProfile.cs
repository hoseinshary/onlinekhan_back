using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.City;

namespace NasleGhalam.ServiceLayer.MapperProfile
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
