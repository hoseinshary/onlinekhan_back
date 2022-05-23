using System;
using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.QuestionGroup
{
    public class QuestionGroupUpdateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "")]
        public string Title { get; set; }

        [Display(Name = "")]
        public DateTime InsertTime { get; set; }

        [Display(Name = "")]
        public string File { get; set; }

        [Display(Name = "")]
        public int LessonId { get; set; }

        [Display(Name = "")]
        public int UserId { get; set; }
    }
}
