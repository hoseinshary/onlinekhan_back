using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels.Question;

namespace NasleGhalam.ViewModels.Assay
{
    public class LessonAssayViewModel
    {
        public int Id { get; set; }

        public string  Name { get; set; }

        public IEnumerable<int> TopicIds { get; set; }

        public IEnumerable<TopicAssayViewModel> Topics { get; set; }

        public IEnumerable<QuestionViewModel> Questions { get; set; }

        [Display(Name = "جای چرک نویس")]
        public bool HaveWhiteSpace { get; set; }

        [Display(Name = "چرک نویس بغل / زیر")]
        public bool WhiteSpacePosition { get; set; }

        public int CountOfEasy { get; set; }

        public int CountOfMedium { get; set; }

        public int CountOfHard { get; set; }

        public int CountOfQuestions => CountOfEasy + CountOfMedium + CountOfHard;

        public int LessonOrder { get; set; }
    }
}
