using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class LessonDepartment
    {

        public LessonDepartment()
        {
            Lessons = new HashSet<Lesson>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Lesson> Lessons { get; set; }
    }
}