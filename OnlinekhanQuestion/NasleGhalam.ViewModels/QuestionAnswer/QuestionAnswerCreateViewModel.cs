using System;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.Common;

namespace NasleGhalam.ViewModels.QuestionAnswer
{
    public class QuestionAnswerCreateViewModel
    {
        public QuestionAnswerCreateViewModel()
        {
            FileName = Guid.NewGuid().ToString();
        }
        [Display(Name = "عنوان")]
        public string Title { get; set; }


        [Display(Name = "متن")]
        public string Context { get; set; }


        [Display(Name = "درس")]
        public string LessonName { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "نویسنده")]
        public int WriterId { get; set; }

     

        [Display(Name = "اصلی")]
        public bool IsMaster { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        [Display(Name = "حذف")]
        public bool IsDelete { get; set; }

        [Display(Name = "ویرایش")]
        public bool IsUpdate { get; set; }

        [Display(Name = "کاربر")]
        public int UserId { get; set; }

        [Display(Name = "سوال")]
        public int QuestionId { get; set; }

        [Display(Name = "نوع جواب")]
        public int LookupId_AnswerType { get; set; }

        public QuestionAnswerType QuestionAnswerType { get; set; }

        public string QuestionAnswerTypeDisplay => QuestionAnswerType.GetDisplayName();

        public string FilePath { get; set; }
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
                catch (Exception exp)
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

    }
}
