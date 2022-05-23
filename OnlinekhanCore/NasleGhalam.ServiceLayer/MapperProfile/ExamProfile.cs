using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Exam;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class ExamProfile : Profile
    {
        public ExamProfile()
        {
            CreateMap<ExamViewModel, Exam>();
        }
    }
}
