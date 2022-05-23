using AutoMapper;
using NasleGhalam.DomainClasses.Entities;


namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class QuestionUpdateProfile : Profile
    {
        public QuestionUpdateProfile()
        {
          


            CreateMap<ViewModels.QuestionUpdate.QuestionUpdateViewModel, QuestionUpdate>();
        }
    }
}