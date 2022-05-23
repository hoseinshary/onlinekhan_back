namespace NasleGhalam.DomainClasses.Entities
{
    public class Ratio
    {
        public Ratio()
        {
        }
        public int Id { get; set; }

        public byte Rate { get; set; }

        public int LessonId { get; set; }

        public Lesson Lesson { get; set; }

        public int EducationSubGroupId { get; set; }

        public EducationSubGroup EducationSubGroup { get; set; }
    }
}
