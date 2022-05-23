using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.QuestionAnswer;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    class QuestionAnswerProfile :Profile
    {
        public QuestionAnswerProfile()
        {
            CreateMap<QuestionAnswerCreateViewModel, QuestionAnswer>();
            CreateMap<QuestionAnswerViewModel, QuestionAnswer>();
            CreateMap<QuestionAnswerUpdateViewModel, QuestionAnswer>();
        }
    }
}
