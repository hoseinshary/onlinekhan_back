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
    public class QuestionReportExcelViewModel
    {
        [Display(Name = "شماره سوال")]
        public int Id { get; set; }
        [Display(Name = "نوع سوال")]
        public string QuestionTypeName { get; set; }
 
       
     
        [Display(Name = "ناظر")]

        public string SupervisorName { get; set; }
    
        [Display(Name = "نویسنده")]

        public string WriterName { get; set; }

      


        [Display(Name = "وضعیت سوال")]

        public string QuestionStatusDisplayName { get; set; }
    
        [Display(Name = "شاخه بند")]

        public string TopicUserName { get; set; }

        [Display(Name = "کارشناس1")]

        public string QuestionJudgedStateDisplayName1 { get; set; }

        [Display(Name = "کارشناس2")]

        public string QuestionJudgedStateDisplayName2 { get; set; }
        [Display(Name = "کارشناس3")]

        public string QuestionJudgedStateDisplayName3 { get; set; }
    
        [Display(Name = "ویراستار")]

        public string EditorUserName { get; set; }

        [Display(Name = "تعداد پاسخنامه")]

        public int NumberOfAnswers { get; set; }

        [Display(Name = "کارشناس1 پاسخ")]

        public string AnswerJudgedStateDisplayName1 { get; set; }

        [Display(Name = " کارشناس2 پاسخ")]

        public string AnswerJudgedStateDisplayName2 { get; set; }

        [Display(Name = "کارشناس3 پاسخ")]

        public string AnswerJudgedStateDisplayName3 { get; set; }
        [Display(Name = "آنلاین خوان")]

        public bool HaveOnlinekhanAnswer { get; set; }

        [Display(Name = "نوسنده پاسخنامه")]

        public string AnswerWriterName { get; set; }
        [Display(Name = "نوع پاسخنامه")]


        public string QuestionAnswerTypeDisplay { get; set; }

     


    }
}
