using System.Collections.Generic;

namespace NasleGhalam.ViewModels.Lesson
{
    public class Lesson_UserViewModel
    {
        public ICollection<int> UserIds { get; set; }

        public ICollection<int> LessonIds { get; set; }
    }
}
