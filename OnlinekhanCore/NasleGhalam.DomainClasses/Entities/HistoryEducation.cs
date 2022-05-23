using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class HistoryEducation
    {
        public HistoryEducation()
        {
            UniversityBranches = new HashSet<UniversityBranch>();
            Cities = new HashSet<City>();
        }
        public int Id { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int RankGoal { get; set; }

        public int ExamId { get; set; }

        public Exam Exam { get; set; }

        public int EducationTreeId { get; set; }

        public EducationTree  EducationTree { get; set; }

        public ICollection<UniversityBranch> UniversityBranches { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
