using NasleGhalam.ViewModels.EducationSubGroup;
using NasleGhalam.ViewModels.EducationTree;
using NasleGhalam.ViewModels.Lesson;

namespace NasleGhalam.ViewModels.Ratio
{
    public class RatioViewModel
    {
        public int Id { get; set; }

        public byte Rate { get; set; }

        public int LessonId { get; set; }

        public LessonViewModel Lesson { get; set; }

        public int EducationSubGroupId { get; set; }

        public EducationSubGroupViewModel EducationSubGroup { get; set; }

        public int EducationTreeId { get; set; }

        public EducationTreeViewModel EducationTree { get; set; }
    }
}
