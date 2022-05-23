using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Tag;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<TagViewModel, Tag>();
        }
    }
}
