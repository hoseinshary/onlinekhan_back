using System.Collections.Generic;


namespace NasleGhalam.DomainClasses.Entities
{
    public class Lookup
    {
        public Lookup()
        {
  

            QuestionAnswers = new HashSet<QuestionAnswer>();

            Question_QuestionHardnessTypes = new HashSet<Question>();
            Question_QuestionTypes = new HashSet<Question>();
            Question_RepeatnessTypes = new HashSet<Question>();
            Question_AreaTypes = new HashSet<Question>();
            Question_AuthorTypes = new HashSet<Question>();
            Question_QuestionRanks = new HashSet<Question>();

        

            Topic_AreaTypes = new HashSet<Topic>();
            Topic_Hardnesses = new HashSet<Topic>();
            Lesson_Nezams = new HashSet<Lesson>();
            EducationTree_States = new HashSet<EducationTree>();

            Assay_Importance = new HashSet<Assay>();
            Assay_QuestionType = new HashSet<Assay>();
            Assay_Type = new HashSet<Assay>();

            ProgramItem_Name = new HashSet<ProgramItem>();

            Media_Type = new HashSet<Media>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public int State { get; set; }

        public ICollection<QuestionAnswer> QuestionAnswers { get; set; }

       

        public ICollection<Question> Question_QuestionTypes { get; set; }

        public ICollection<Question> Question_QuestionHardnessTypes { get; set; }
        public ICollection<Question> Question_RepeatnessTypes { get; set; }
        public ICollection<Question> Question_AreaTypes { get; set; }
        public ICollection<Question> Question_AuthorTypes { get; set; }

        public ICollection<Question> Question_QuestionRanks { get; set; }

    

        public ICollection<Topic> Topic_Hardnesses { get; set; }

        public ICollection<Topic> Topic_AreaTypes { get; set; }

        public ICollection<Lesson> Lesson_Nezams { get; set; }

        public ICollection<EducationTree> EducationTree_States { get; set; }

        public ICollection<Assay> Assay_Importance { get; set; }

        public ICollection<Assay> Assay_Type { get; set; }

        public ICollection<Assay> Assay_QuestionType { get; set; }

        public ICollection<ProgramItem> ProgramItem_Name { get; set; }

        public ICollection<Media> Media_Type { get; set; }
    }
}
