using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Resume
{
    public class TeachingResumeViewModel
    {
        [Display(Name = "آموزشگاه")]
        public string School { get; set; }

        [Display(Name = "درس")]
        public string LessonName { get; set; }

        [Display(Name = "مقطع")]
        public string Grade { get; set; }

        [Display(Name = "سال شروع ")]
        public string StartYear { get; set; }

        [Display(Name = "سال پایان")]
        public string EndYear { get; set; }
        
    }
}
