using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Package;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class PackageProfile : Profile
    {
        public PackageProfile()
        {
            CreateMap<PackageViewModel, Package>();
            CreateMap<PackageCreateViewModel, Package>();
            CreateMap<PackageUpdateViewModel, Package>();
        }
    }
}
