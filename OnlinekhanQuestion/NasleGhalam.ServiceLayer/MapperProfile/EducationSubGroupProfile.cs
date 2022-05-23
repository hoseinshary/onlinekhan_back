using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.EducationSubGroup;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class EducationSubGroupProfile : Profile
    {
        public EducationSubGroupProfile()
        {
            CreateMap<EducationSubGroupViewModel, EducationSubGroup>();
            CreateMap<EducationSubGroupUpdateViewModel, EducationSubGroup>();
            CreateMap<EducationSubGroupCreateViewModel, EducationSubGroup>();
        }
    }
}
