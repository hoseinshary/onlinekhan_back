using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Lesson
    {
        public Lesson()
        {
            Ratios = new HashSet<Ratio>();
            EducationTrees = new HashSet<EducationTree>();
            Topics = new HashSet<Topic>();
            EducationBooks = new HashSet<EducationBook>();
            Users = new HashSet<User>();
            LessonDepartments = new HashSet<LessonDepartment>();
            Assays = new HashSet<Assay>();
            Packages = new HashSet<Package>();


        }
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsMain { get; set; }

        public int LookupId_Nezam { get; set; }

        public int NumberOfJudges { get; set; }

        public Lookup Lookup_Nezam { get; set; }

        public string File { get; set; }

        public ICollection<Ratio> Ratios { get; set; }

        public ICollection<EducationTree> EducationTrees { get; set; }

        public ICollection<Topic> Topics { get; set; }

        public ICollection<QuestionGroup> QuestionGroups { get; set; }

        public ICollection<EducationBook> EducationBooks { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<LessonDepartment> LessonDepartments { get; set; }

        public ICollection<Assay> Assays { get; set; }

        public ICollection<Package> Packages { get; set; }




    }
}
