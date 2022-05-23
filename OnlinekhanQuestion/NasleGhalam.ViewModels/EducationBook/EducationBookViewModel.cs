using System.Collections.Generic;
using NasleGhalam.ViewModels.Topic;

namespace NasleGhalam.ViewModels.EducationBook
{
    public class EducationBookViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public short PublishYear { get; set; }

        public bool IsExamSource { get; set; }

        public bool IsActive { get; set; }

        public bool IsChanged { get; set; }

        public int LessonId { get; set; }

        //public LessonViewModel Lesson { get; set; }

        public ICollection<TopicViewModel> Topics { get; set; }
    }
}
