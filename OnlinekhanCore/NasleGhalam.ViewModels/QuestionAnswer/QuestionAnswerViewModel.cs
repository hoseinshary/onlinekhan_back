using System.ComponentModel.DataAnnotations;
using NasleGhalam.Common;
using NasleGhalam.ViewModels.Writer;

namespace NasleGhalam.ViewModels.QuestionAnswer
{
    public class QuestionAnswerViewModel
    {
        public int Id { get; set; }


        [Display(Name = "عنوان")]
        public string Title { get; set; }


        [Display(Name = "متن")]
        public string Context { get; set; }


        [Display(Name = "آدرس فایل")]
        public string FilePath { get; set; }

        public string QuestionWordPath => $"/Api/QuestionAnswer/GetWordFile/{FilePath}".ToFullRelativePath();

        public string QuestionAnswerPicturePath => $"/Api/QuestionAnswer/GetPictureFile/{FilePath}".ToFullRelativePath();

        [Display(Name = "نوع پاسخ")]
        public int LookupId_AnswerType { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        [Display(Name = "حذف")]
        public bool IsDelete { get; set; }

        [Display(Name = "ویرایش")]
        public bool IsUpdate { get; set; }

        [Display(Name = "درس")]
        public string LessonName { get; set; }


        [Display(Name = "توضیحات")]
        public string Description { get; set; }


        [Display(Name = "نویسنده")]
        public int WriterId { get; set; }

        public WriterViewModel Writer { get; set; }




        public QuestionAnswerType QuestionAnswerType { get; set; }

        public string QuestionAnswerTypeDisplay => QuestionAnswerType.GetDisplayName();

        [Display(Name = "اصلی")]
        public bool IsMaster { get; set; }


        [Display(Name = "کاربر")]
        public int UserId { get; set; }


        [Display(Name = "سوال")]
        public int QuestionId { get; set; }
    }
}
