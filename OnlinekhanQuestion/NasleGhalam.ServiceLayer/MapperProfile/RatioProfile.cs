using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Ratio;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class RatioProfile : Profile
    {
        public RatioProfile()
        {
            CreateMap<RatioViewModel, Ratio>().ReverseMap();
            CreateMap<RatioCreateViewModel, Ratio>();
            CreateMap<RatioUpdateViewModel, Ratio>();
        }
    }
}
