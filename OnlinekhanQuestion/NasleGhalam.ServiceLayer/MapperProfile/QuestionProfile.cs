using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Question;
using NasleGhalam.ViewModels.QuestionOption;
using System.Collections.Generic;
using System.Linq;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<QuestionCreateViewModel, Question>();
            CreateMap<QuestionCreateWindowsViewModel, Question>();
            CreateMap<QuestionViewModel, Question>()
                .ForMember(dst => dst.TopicAnswer, opt => opt.MapFrom(src => string.Join(",", src.TopicAnswer)))
                .ReverseMap()
                .ForMember(dst => dst.TopicAnswer, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.TopicAnswer) ? new List<string>() : src.TopicAnswer.Split(',').ToList()));
                
            CreateMap<QuestionOptionViewModel, QuestionOption>();


        }
    }
}
