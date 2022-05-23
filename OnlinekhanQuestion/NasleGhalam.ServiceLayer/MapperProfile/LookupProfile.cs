using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Lookup;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class LookupProfile : Profile
    {
        public LookupProfile()
        {
            CreateMap<LookupViewModel, Lookup>();
        }
    }
}
