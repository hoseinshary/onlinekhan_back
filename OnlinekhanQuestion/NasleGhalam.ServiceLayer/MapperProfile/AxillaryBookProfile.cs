using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.AxillaryBook;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class AxillaryBookProfile : Profile
    {
        public AxillaryBookProfile()
        {
            CreateMap<AxillaryBookViewModel, AxillaryBook>();
            
        }
    }
}
