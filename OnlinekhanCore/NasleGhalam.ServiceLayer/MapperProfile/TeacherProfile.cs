using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Teacher;
using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<TeacherCreateViewModel, Teacher>();
            CreateMap<TeacherUpdateViewModel, Teacher>();
            CreateMap<UserCreateViewModel, Teacher>();

            CreateMap<TeacherViewModel, Teacher>()
                .ReverseMap()
                .ForPath(dst => dst.User.ProvinceId, opt => opt.MapFrom(src => src.User.City.ProvinceId))
                .ForMember(dst => dst.User, opt => opt.MapFrom(src => src.User));
        }
    }
}
