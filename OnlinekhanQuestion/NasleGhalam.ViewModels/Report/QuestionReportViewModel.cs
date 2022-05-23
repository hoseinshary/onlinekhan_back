using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NasleGhalam.Common;
using NasleGhalam.ViewModels.Lookup;
using NasleGhalam.ViewModels.QuestionAnswer;
using NasleGhalam.ViewModels.QuestionJudge;
using NasleGhalam.ViewModels.QuestionOption;
using NasleGhalam.ViewModels.Tag;
using NasleGhalam.ViewModels.Topic;
using NasleGhalam.ViewModels.User;
using NasleGhalam.ViewModels.Writer;

namespace NasleGhalam.ViewModels.Question
{
    public class QuestionReportViewModel
    {
        [Display(Name = "شماره سوال")]
        public int Id { get; set; }
        [Display(Name = "نوع سوال")]
        public string QuestionTypeName { get; set; }
        [Display(Name = "نوع سوال")]
        public int LookupId_QuestionType { get; set; }
        [Display(Name = "حذف")]
        public bool IsDelete { get; set; }
        [Display(Name = "فعال")]

        public bool IsActive { get; set; }
        [Display(Name = "ویرایش")]

        public bool IsUpdate { get; set; }
        [Display(Name = "کاربر ورود سوال")]

        public int UserId { get; set; }
        [Display(Name = "کاربر ورود سوال")]

        public string UserName { get; set; }
        [Display(Name = "ناظر")]

        public int SupervisorUserId { get; set; }
        [Display(Name = "ناظر")]

        public string SupervisorName { get; set; }
        [Display(Name = "نویسنده")]

        public int WriterId { get; set; }

        [Display(Name = "نویسنده")]

        public string WriterName { get; set; }

        [Display(Name = "نوع نویسنده")]

        public int LookupId_AuthorType { get; set; }
        [Display(Name = "نوع نویسنده")]

        public string AuthorTypeName { get; set; }
        [Display(Name = "وضعیت سوال")]

        public QuestionStatus QuestionStatus
        {
            get
            {
                if (Topics.Count == 0)
                    return QuestionStatus.Imported;
                else if (QuestionJudges.Count == 0)
                    return QuestionStatus.Topiced;
                else if (QuestionJudges.Count < Topics.First().Lesson.NumberOfJudges)
                    return QuestionStatus.JudgedInComplete;
                else if (IsActive)
                    return QuestionStatus.JudgedActive;
                else
                    return QuestionStatus.JudgedInActive;
            }
        }
        [Display(Name = "وضعیت سوال")]

        public string QuestionStatusDisplayName => QuestionStatus.GetDisplayName();
        [Display(Name = "شاخه بند")]

        public int TopicUserId { get; set; }
        [Display(Name = "شاخه بند")]

        public string TopicUserName { get; set; }
        [Display(Name = "کارشناس1")]

        public QuestionJudgedState QuestionJudgedState1 { get; set; }
        [Display(Name = "کارشناس1")]

        public string QuestionJudgedStateDisplayName1 => QuestionJudgedState1.GetDisplayName();
        [Display(Name = "کارشناس2")]

        public QuestionJudgedState QuestionJudgedState2 { get; set; }
        [Display(Name = "کارشناس2")]

        public string QuestionJudgedStateDisplayName2 => QuestionJudgedState2.GetDisplayName();
        [Display(Name = "کارشناس3")]

        public QuestionJudgedState QuestionJudgedState3 { get; set; }
        [Display(Name = "کارشناس3")]

        public string QuestionJudgedStateDisplayName3 => QuestionJudgedState3.GetDisplayName();

        [Display(Name = "ویراستار")]

        public int EditorUserId { get; set; }
        [Display(Name = "ویراستار")]

        public string EditorUserName { get; set; }

        [Display(Name = "تعداد پاسخنامه")]

        public int NumberOfAnswers { get; set; }

        [Display(Name = "کارشناس1")]

        public QuestionJudgedState AnswerJudgedState1 { get; set; }
        [Display(Name = "کارشناس1")]

        public string AnswerJudgedStateDisplayName1 => AnswerJudgedState1.GetDisplayName();
        [Display(Name = "کارشناس2")]

        public QuestionJudgedState AnswerJudgedState2 { get; set; }
        [Display(Name = "کارشناس2")]

        public string AnswerJudgedStateDisplayName2 => AnswerJudgedState2.GetDisplayName();
        [Display(Name = "کارشناس3")]

        public QuestionJudgedState AnswerJudgedState3 { get; set; }
        [Display(Name = "کارشناس3")]

        public string AnswerJudgedStateDisplayName3 => AnswerJudgedState3.GetDisplayName();
        [Display(Name = "آنلاین خوان")]

        public bool HaveOnlinekhanAnswer { get; set; }
        [Display(Name = "نوسنده پاسخنامه")]

        public int AnswerWriterId { get; set; }

        [Display(Name = "نوسنده پاسخنامه")]

        public string AnswerWriterName { get; set; }
        [Display(Name = "نوع پاسخنامه")]

        public QuestionAnswerType QuestionAnswerType { get; set; }
        [Display(Name = "نوع پاسخنامه")]

        public string QuestionAnswerTypeDisplay => QuestionAnswerType.GetDisplayName();

        public List<QuestionJudgeViewModel> QuestionJudges { get; set; } = new List<QuestionJudgeViewModel>();

        public List<TopicViewModel> Topics { get; set; } = new List<TopicViewModel>();


    }
}
