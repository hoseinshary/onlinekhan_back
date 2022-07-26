using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Tag;
using NasleGhalam.ViewModels.Teacher;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class ErrorProfile : Profile
    {
        public ErrorProfile()
        {
            CreateMap<ErrorCreateViewModel, Error>();
        }
    }
}
