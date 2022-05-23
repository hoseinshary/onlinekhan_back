using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Publisher;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<PublisherViewModel, Publisher>().ReverseMap();
        }
    }
}
