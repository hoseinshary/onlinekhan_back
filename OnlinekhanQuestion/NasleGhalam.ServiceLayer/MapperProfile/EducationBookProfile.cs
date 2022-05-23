using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.EducationBook;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class EducationBookProfile : Profile
    {
        public EducationBookProfile()
        {
            CreateMap<EducationBookViewModel, EducationBook>().ReverseMap();
            CreateMap<EducationBookCreateViewModel, EducationBook>();
            CreateMap<EducationBookUpdateViewModel, EducationBook>();
        }
    }
}
