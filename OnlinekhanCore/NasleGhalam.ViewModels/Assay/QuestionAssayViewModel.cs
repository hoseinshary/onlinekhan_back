using System.Collections.Generic;
using NasleGhalam.ViewModels.Question;

namespace NasleGhalam.ViewModels.Assay
{
    public class QuestionAssayViewModel
    {
       public  QuestionAssayViewModel()
        {
            Questions = new List<QuestionViewModel>();
        }

        public int LessonId { get; set; }

        public List<QuestionViewModel> Questions { get; set; }
    }
}
