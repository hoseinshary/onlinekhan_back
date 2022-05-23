using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.StudentMajorlist;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class StudentMajorlistProfile : Profile
    {
        public StudentMajorlistProfile()
        {
            CreateMap<StudentMajorlistViewModel, StudentMajorlist>();
            CreateMap<StudentMajorlistGetStudentViewModel, StudentMajorlist>();
            
            CreateMap<StudentMajorlistUpdateViewModel, StudentMajorlist>();
            CreateMap<MajorViewModel, StudentMajorlist>();
            CreateMap<MajorViewModel, Majors>();
            CreateMap<MajorViewModel, StudentMajorList_Major>();
        }
    }
}
