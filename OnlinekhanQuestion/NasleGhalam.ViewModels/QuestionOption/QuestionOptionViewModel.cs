using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.QuestionOption
{
    public class QuestionOptionViewModel
    {
        [Display(Name = "")]
        public string Context { get; set; }

        [Display(Name = "")]
        public bool IsAnswer { get; set; }
    }
}
