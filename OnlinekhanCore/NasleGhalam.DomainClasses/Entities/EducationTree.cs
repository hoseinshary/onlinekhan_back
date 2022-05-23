using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class EducationTree
    {
        public EducationTree()
        {
       
            Lessons = new HashSet<Lesson>();
            ChildrenEducationTree = new HashSet<EducationTree>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentEducationTreeId { get; set; }

        public int LookupId_EducationTreeState { get; set; }

        public Lookup Lookup_EducationTreeState { get; set; }

        public EducationTree ParentEducationTree { get; set; }

      
    
        public ICollection<Lesson> Lessons { get; set; }

    

        public ICollection<EducationTree> ChildrenEducationTree { get; set; }

    }
}
