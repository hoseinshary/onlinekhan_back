using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Program;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class ProgramProfile : Profile
    {
        public ProgramProfile()
        {
            CreateMap<ProgramViewModel, Program>();
            CreateMap<ProgramCreateViewModel, Program>();
        }
    }
}
