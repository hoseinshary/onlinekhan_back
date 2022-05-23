using System;
using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Exam
    {
        public Exam()
        {
            HistoryEducations = new HashSet<HistoryEducation>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public int EducationTreeId { get; set; }

        public EducationTree EducationTree { get; set; }

        public int EducationYearId { get; set; }

        public EducationYear EducationYear { get; set; }

        public ICollection<HistoryEducation> HistoryEducations { get; set; }

    }
}
