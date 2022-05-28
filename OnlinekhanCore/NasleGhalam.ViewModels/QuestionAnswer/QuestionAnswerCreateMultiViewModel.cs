using System.ComponentModel.DataAnnotations;
using NasleGhalam.Common;

namespace NasleGhalam.ViewModels.QuestionAnswer
{
    public class QuestionAnswerCreateMultiViewModel
    {
 
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "نویسنده")]
        public int WriterId { get; set; }


        public int UserId { get; set; }

        [Display(Name = "سوال گروهی")]
        public int QuestionGroupId { get; set; }


       

    }
}
