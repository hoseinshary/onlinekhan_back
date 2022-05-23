using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using NasleGhalam.ViewModels._Attributes;

namespace NasleGhalam.ViewModels.Question
{
    public class QuestionUpdateViewModel
    {
        public int Id { get; set; }


        [Display(Name = "متن")]
        public string Context { get; set; }


        [Display(Name = "شماره سوال ")]
        public int QuestionNumber { get; set; }

        [Display(Name = "نوع سوال ")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_QuestionType { get; set; }

        [Display(Name = "نمره")]
        public int QuestionPoint { get; set; }

        [Display(Name = "درجه سختی")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_QuestionHardnessType { get; set; }

        [Display(Name = "درجه تکرار")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_RepeatnessType { get; set; }

        [Display(Name = "ارزیابی")]
        public bool UseEvaluation { get; set; }

        [Display(Name = "استاندارد")]
        public bool IsStandard { get; set; }

        [Display(Name = "نوع طراح")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_AuthorType { get; set; }

        [Display(Name = "نویسنده")]
        public int WriterId { get; set; }

        [Display(Name = "فایل")]
        public string Base64File { get; set; }

        public byte[] FileBytes
        {
            get
            {
                try
                {
                    if (!string.IsNullOrEmpty(Base64File))
                    {
                        return Convert.FromBase64String(Base64File.Replace(
                            Base64File.Substring(0, Base64File.IndexOf("base64,", StringComparison.Ordinal) + 7), ""));
                    }
                }
                catch
                {
                    // ignored
                }

                return new byte[] { };
            }
        }


        [Display(Name = "نوع طراح")]
        public List<int> LookupId_AreaTypes { get; set; }


        [Display(Name = "امتیاز")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_QuestionRank { get; set; }

        [Display(Name = "زمان پاسخ")]
        public short ResponseSecond { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        //hatman por shavad 
        [Display(Name = "نام فایل")]
        public string FileName { get; set; }

        [Display(Name = "تاریخ ورود داده")]
        public DateTime InsertDateTime { get; set; }

        [Display(Name = "کاربر")]
        public int UserId { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        [Display(Name = "سوال ترکیبی")]
        public bool IsHybrid { get; set; }


        [Display(Name = "حذف")]
        public bool IsDelete { get; set; }

        public int AnswerNumber { get; set; }

        [Display(Name = "ناظر")]
        public int SupervisorUserId { get; set; }

        [Display(Name = "مبحث پاسخ صحیح")]
        public string TopicAnswer { get; set; }

        public List<int> TopicIds { get; set; } = new List<int>();

        public List<int> TagIds { get; set; } = new List<int>();
    }
}
