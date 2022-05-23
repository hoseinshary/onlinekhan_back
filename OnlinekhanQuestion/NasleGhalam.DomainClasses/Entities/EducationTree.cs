using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class EducationTree
    {
        public EducationTree()
        {
            Exams = new HashSet<Exam>();
            EducationSubGroups = new HashSet<EducationSubGroup>();
            HistoryEducations = new HashSet<HistoryEducation>();
            Lessons = new HashSet<Lesson>();
            ChildrenEducationTree = new HashSet<EducationTree>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentEducationTreeId { get; set; }

        public int LookupId_EducationTreeState { get; set; }

        public Lookup Lookup_EducationTreeState { get; set; }

        public EducationTree ParentEducationTree { get; set; }

        public ICollection<HistoryEducation> HistoryEducations { get; set; }
    
        public ICollection<Lesson> Lessons { get; set; }

        public ICollection<EducationSubGroup> EducationSubGroups { get; set; }

        public ICollection<Exam> Exams { get; set; }

        public ICollection<EducationTree> ChildrenEducationTree { get; set; }

    }
}
