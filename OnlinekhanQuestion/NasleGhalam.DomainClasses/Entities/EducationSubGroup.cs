using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class EducationSubGroup
    {
        public EducationSubGroup()
        {
            UniversityBranches = new HashSet<UniversityBranch>();
            Ratios = new HashSet<Ratio>();
            HistoryEducations = new HashSet<HistoryEducation>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int EducationTreeId { get; set; }

        public EducationTree EducationTree { get; set; }

        public ICollection<UniversityBranch> UniversityBranches { get; set; }

        public ICollection<Ratio> Ratios { get; set; }

        public ICollection<HistoryEducation> HistoryEducations { get; set; }

    }
}
