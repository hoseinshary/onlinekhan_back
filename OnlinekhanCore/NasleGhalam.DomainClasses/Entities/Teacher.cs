using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Teacher
    {
        public Teacher()
        {
            Boxes = new HashSet<Box>();
            TeacherGroups = new HashSet<TeacherGroup>();
        }
        
        public int Id { get; set; }

        public string FatherName { get; set; }

        public string Address { get; set; }

        

        public User User { get; set; }

        public ICollection<Box> Boxes { get; set; }
        public ICollection<TeacherGroup> TeacherGroups { get; set; }
    }
}
