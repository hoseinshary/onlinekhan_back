using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.UniversityBranch;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class UnivercityBranchProfile : Profile
    {
        public UnivercityBranchProfile()
        {
            CreateMap<UniversityBranchViewModel, UniversityBranch>();
        }
    }
}
