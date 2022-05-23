using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class UniversityBranch
    {
        public UniversityBranch()
        {
            HistoryEducations = new HashSet<HistoryEducation>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int SiteAverage { get; set; }

        public int Balance1Low { get; set; }

        public int Balance1High { get; set; }

        public int Balance2Low { get; set; }

        public int Balance2High { get; set; }

        public int EducationSubGroupId { get; set; }

        public EducationSubGroup EducationSubGroup { get; set; }

        public ICollection<HistoryEducation> HistoryEducations { get; set; }
    }
}
