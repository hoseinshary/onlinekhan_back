using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class EducationYear
    {
        public EducationYear()
        {
            Exams = new HashSet<Exam>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsActiveYear { get; set; }

        public ICollection<Exam> Exams { get; set; }
    }
}
