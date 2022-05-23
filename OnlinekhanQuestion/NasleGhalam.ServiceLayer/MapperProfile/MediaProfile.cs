using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Media;
using NasleGhalam.ViewModels.Role;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class MediaProfile : Profile
    {
        public MediaProfile()
        {
            CreateMap<Media, MediaViewModel>().ReverseMap();
            CreateMap<MediaCreateViewModel, Media>();
            CreateMap<MediaUpdateViewModel, Media>();
        }
    }
}
