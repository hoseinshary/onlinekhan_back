using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Student;
using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentCreateViewModel, Student>();
            CreateMap<StudentUpdateViewModel, Student>();
            CreateMap<UserCreateViewModel, Student>();
            CreateMap<StudentViewModel, Student>()
                .ReverseMap()
                .ForPath(dst => dst.User.ProvinceId, opt => opt.MapFrom(src => src.User.City.ProvinceId))
                .ForMember(dst => dst.User, opt => opt.MapFrom(src => src.User));
        }
    }
}
