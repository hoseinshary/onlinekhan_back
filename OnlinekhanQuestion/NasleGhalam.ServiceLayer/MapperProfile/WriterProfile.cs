using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Writer;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class WriterProfile : Profile
    {
        public WriterProfile()
        {
            CreateMap<WriterCreateViewModel, Writer>();
            CreateMap<WriterUpdateViewModel, Writer>();
            CreateMap<WriterViewModel, Writer>();

        }
    }
}
