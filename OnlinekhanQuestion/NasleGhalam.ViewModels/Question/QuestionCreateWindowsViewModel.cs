using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using NasleGhalam.ViewModels._Attributes;
using NasleGhalam.ViewModels.QuestionGroup;
using NasleGhalam.ViewModels.QuestionOption;

namespace NasleGhalam.ViewModels.Question
{
    public class QuestionCreateWindowsViewModel
    {
        public QuestionCreateWindowsViewModel()
        {
            FileName = Guid.NewGuid().ToString();
            InsertDateTime = DateTime.Now;
        }

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



        public int QuestionGroupId { get; set; }



        [Display(Name = "نوع طراح")]
        
        public List<int> LookupId_AreaTypes { get; set; }

        [Display(Name = "امتیاز")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_QuestionRank { get; set; }
        

        [Display(Name = "زمان پاسخ")]
        public short ResponseSecond { get; set; }

        [MaxLength(300, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        [Display(Name = "توضیحات")]
        public string Description { get; set; }


        //hatman por shavad 
        [Display(Name = "نام فایل")]
        public string FileName { get; set; }


        [Display(Name = "فایل")]
        public string WordBase64File { get; set; }

        public byte[] WordFileBytes
        {
            get
            {
                try
                {
                    if (!string.IsNullOrEmpty(WordBase64File))
                    {
                        return Convert.FromBase64String(WordBase64File);
                        //return Convert.FromBase64String(WordBase64File.Replace(
                         //   WordBase64File.Substring(0, WordBase64File.IndexOf("base64,", StringComparison.Ordinal) + 7), ""));
                    }
                }
                catch
                {
                    // ignored
                }

                return new byte[] { };
            }
        }



        [Display(Name = "فایل")]
        public string PngBase64File { get; set; }

        public byte[] PngFileBytes
        {
            get
            {
                try
                {
                    if (!string.IsNullOrEmpty(PngBase64File))
                    {
                        return Convert.FromBase64String(PngBase64File);
                        //return Convert.FromBase64String(PngBase64File.Replace(
                       //     PngBase64File.Substring(0, PngBase64File.IndexOf("base64,", StringComparison.Ordinal) + 7), ""));
                    }
                }
                catch
                {
                    // ignored
                }

                return new byte[] { };
            }
        }

        public string FilePath { get; set; }



        [Display(Name = "تاریخ ورود داده")]
        public DateTime InsertDateTime { get; }

        [Display(Name = "کاربر")]
        public int UserId { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        [Display(Name = "سوال ترکیبی")]
        public bool IsHybrid { get; set; }

   

        [Display(Name = "گزینه صحیح")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [Range(0, 4, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Range")]
        public int AnswerNumber { get; set; }

        [Display(Name = "مبحث پاسخ صحیح")]
        public string TopicAnswer { get; set; }

        [Display(Name = "ناظر")]
        public int SupervisorUserId { get; set; }

        public List<int> TopicIds { get; set; } = new List<int>();

        public List<int> TagIds { get; set; } = new List<int>();

        public List<QuestionOptionViewModel> Options { get; set; } = new List<QuestionOptionViewModel>();
        public List<QuestionGroupViewModel> QuestionGroups { get; set; } = new List<QuestionGroupViewModel>();
    }
}
