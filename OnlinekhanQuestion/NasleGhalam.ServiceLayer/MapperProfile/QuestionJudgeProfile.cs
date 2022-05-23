using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.QuestionJudge;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class QuestionJudgeProfile : Profile
    {
        public QuestionJudgeProfile()
        {
            CreateMap<QuestionJudgeCreateViewModel, QuestionJudge>();
            CreateMap<QuestionJudgeUpdateViewModel, QuestionJudge>();
            CreateMap<QuestionJudgeViewModel, QuestionJudge>();
            
        }
    }
}
