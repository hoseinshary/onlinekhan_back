using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.QuestionAnswerJudge;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    class QuestionAnswerJudgeProfile :Profile
    {
        public QuestionAnswerJudgeProfile()
        {
            CreateMap<QuestionAnswerJudgeCreateViewModel, QuestionAnswerJudge>();
            CreateMap<QuestionAnswerJudgeViewModel, QuestionAnswerJudge>();
            CreateMap<QuestionAnswerJudgeUpdateViewModel, QuestionAnswerJudge>();
        }
    }
}
