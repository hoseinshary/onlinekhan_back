using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.EducationYear;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class EducationYearProfile : Profile
    {
        public EducationYearProfile()
        {
            CreateMap<EducationYearViewModel, EducationYear>();
            CreateMap<EducationYearCreateViewModel, EducationYear>();
            CreateMap<EducationYearUpdateViewModel, EducationYear>();
        }
    }
}
