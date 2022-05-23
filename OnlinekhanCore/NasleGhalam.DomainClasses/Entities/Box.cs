using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Box
    {
        public Box()
        {
            Questions = new HashSet<Question>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
