using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Assay;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class AssayProfile : Profile
    {
        public AssayProfile()
        {

            CreateMap<QuestionAssayViewModel, Question>();
            CreateMap<LessonAssayViewModel, Lesson>();
            CreateMap<AssayViewModel, Assay>().ForMember(dst => dst.QuestionsFile1, opt => opt.MapFrom(src => string.Join(";", src.QuestionsFile)))
                .ForMember(dst => dst.QuestionsAnswer1, opt => opt.MapFrom(src => string.Join(";", src.QuestionsAnswer)))
                .ReverseMap()
                .ForMember(dst => dst.QuestionsFile, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.QuestionsFile1) ? new List<string>() : src.QuestionsFile1.Split(';').ToList()))
                .ForMember(dst => dst.QuestionsAnswer, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.QuestionsAnswer1) ? new List<string>() : src.QuestionsAnswer1.Split(';').ToList()));
            CreateMap<AssayCreateViewModel, Assay>();


        }
    }
}
