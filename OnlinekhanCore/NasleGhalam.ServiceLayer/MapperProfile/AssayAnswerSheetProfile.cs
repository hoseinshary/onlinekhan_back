using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.AssayAnswerSheet;
using NasleGhalam.Common;


namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class AssayAnswerSheetProfile : Profile
    {
        public AssayAnswerSheetProfile()
        {
            
            CreateMap<AssayAnswerSheetCreateViewModel, AssayAnswerSheet>().ForMember(dst => dst.Answers,
                opt => opt.MapFrom(src => string.Join(";", src.Answers)))
                .ForMember(dst => dst.AfterList, opt => opt.MapFrom(src => string.Join(";" , src.AfterList.ToInts())))
                .ForMember(dst => dst.CantList, opt => opt.MapFrom(src => string.Join(";", src.CantList.ToInts())))
                .ForMember(dst => dst.MaybeList, opt => opt.MapFrom(src => string.Join(";", src.MaybeList.ToInts())));

            CreateMap<AssayAnswerSheetViewModel, AssayAnswerSheet>().ForMember(dst => dst.Answers,
                    opt => opt.MapFrom(src => string.Join(";", src.Answers)))
                .ForMember(dst => dst.AfterList, opt => opt.MapFrom(src => string.Join(";", src.AfterList.ToInts())))
                .ForMember(dst => dst.CantList, opt => opt.MapFrom(src => string.Join(";", src.CantList.ToInts())))
                .ForMember(dst => dst.MaybeList, opt => opt.MapFrom(src => string.Join(";", src.MaybeList.ToInts())))
                .ReverseMap()
                .ForMember(x => x.AnswerSheetCorectExams, opt => opt.Ignore())
                .ForMember(dst => dst.Answers,
                    opt => opt.MapFrom(src =>
                        string.IsNullOrEmpty(src.Answers) ? new List<string>() : src.Answers.Split(';').ToList()));
                //.ForMember(dst => dst.CantList, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.CantList) ? new List<string>() : src.CantList.Split(';').ToList()))
                //.ForMember(dst => dst.AfterList, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.AfterList) ? new List<string>() : src.AfterList.Split(';').ToList()))
                //.ForMember(dst => dst.MaybeList, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.MaybeList) ? new List<string>() : src.MaybeList.Split(';').ToList())); 
            CreateMap<AssayAnswerSheetUpdateViewModel, AssayAnswerSheet>();
        }
    }
}
