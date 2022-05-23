using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using NasleGhalam.ViewModels._Attributes;

namespace NasleGhalam.ViewModels.Question
{
    public class QuestionUpdateImportViewModel
    {
        public int Id { get; set; }      

        [Display(Name = "شماره سوال ")]
        public int QuestionNumber { get; set; }

        [Display(Name = "نوع سوال ")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_QuestionType { get; set; }

        [Display(Name = "نوع طراح")]
        public List<int> LookupId_AreaTypes { get; set; }


        [Display(Name = "نویسنده")]
        public int WriterId { get; set; }

        [Display(Name = "حذف")]
        public bool IsDelete { get; set; }



        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "نام فایل")]
        public string FileName { get; set; }

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


        [Display(Name = "سوال ترکیبی")]
        public bool IsHybrid { get; set; }

        public int AnswerNumber { get; set; }

        [Display(Name = "ناظر")]
        public int SupervisorUserId { get; set; }

        [Display(Name = "کاربر")]
        public int UserId { get; set; }


        [Display(Name = "مبحث پاسخ صحیح")]
        public string TopicAnswer { get; set; }

        public List<int> TagIds { get; set; } = new List<int>();
    }
}
