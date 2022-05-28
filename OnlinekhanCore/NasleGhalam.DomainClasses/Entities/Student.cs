using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Student
    {
        public Student()
        {
            QuestionAnswerViews = new HashSet<QuestionAnswerView>();
            Programs = new HashSet<Program>();
            StudentMajorlists = new HashSet<StudentMajorlist>();
            TeacherGroups = new HashSet<TeacherGroup>();
        }
        public int Id { get; set; }

        public string FatherName { get; set; }

        public string Address { get; set; }

        public User User { get; set; }


        public ICollection<QuestionAnswerView> QuestionAnswerViews { get; set; }
        
        public ICollection<Program> Programs { get; set; }
        public ICollection<StudentMajorlist> StudentMajorlists { get; set; }
        public ICollection<TeacherGroup> TeacherGroups { get; set; }
    }
}
