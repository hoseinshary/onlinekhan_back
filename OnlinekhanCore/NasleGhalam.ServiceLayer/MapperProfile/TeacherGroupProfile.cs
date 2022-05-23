using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.TeacherGroup;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class TeacherGroupProfile : Profile
    {
        public TeacherGroupProfile()
        {
            CreateMap<TeacherGroupViewModel, TeacherGroup>();
            CreateMap<TeacherGroupCreateViewModel, TeacherGroup>();
            CreateMap<TeacherGroupUpdateViewModel, TeacherGroup>();

        }
    }
}
