using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Province;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class ProvinceProfile : Profile
    {
        public ProvinceProfile()
        {
            CreateMap<ProvinceViewModel, Province>();
        }
    }
}
