using System.Collections.Generic;


namespace NasleGhalam.DomainClasses.Entities
{
    public class Lookup
    {
        public Lookup()
        {
            AxillaryBook_PrintTypes = new HashSet<AxillaryBook>();
            AxillaryBook_BookTypes = new HashSet<AxillaryBook>();
            AxillaryBook_PaperTypes = new HashSet<AxillaryBook>();

            QuestionAnswers = new HashSet<QuestionAnswer>();

            Question_QuestionHardnessTypes = new HashSet<Question>();
            Question_QuestionTypes = new HashSet<Question>();
            Question_RepeatnessTypes = new HashSet<Question>();
            Question_AreaTypes = new HashSet<Question>();
            Question_AuthorTypes = new HashSet<Question>();
            Question_QuestionRanks = new HashSet<Question>();

            QuestionJudge_QuestionHardnessTypes = new HashSet<QuestionJudge>();
            QuestionJudge_RepeatnessTypes = new HashSet<QuestionJudge>();
            QuestionJudge_WhereProblem = new HashSet<QuestionJudge>();
            QuestionJudge_ReasonProblem = new HashSet<QuestionJudge>();
            QuestionAnswerJudge_ReasonProblem = new HashSet<QuestionAnswerJudge>();
            QuestionJudge_QuestionRanks = new HashSet<QuestionJudge>();

            Topic_AreaTypes = new HashSet<Topic>();
            Topic_Hardnesses = new HashSet<Topic>();
            Lesson_Nezams = new HashSet<Lesson>();
            EducationTree_States = new HashSet<EducationTree>();

         

            Media_Type = new HashSet<Media>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public int State { get; set; }

        public ICollection<QuestionAnswer> QuestionAnswers { get; set; }

        public ICollection<AxillaryBook> AxillaryBook_PrintTypes { get; set; }

        public ICollection<AxillaryBook> AxillaryBook_BookTypes { get; set; }

        public ICollection<AxillaryBook> AxillaryBook_PaperTypes { get; set; }

        public ICollection<Question> Question_QuestionTypes { get; set; }

        public ICollection<Question> Question_QuestionHardnessTypes { get; set; }
        public ICollection<Question> Question_RepeatnessTypes { get; set; }
        public ICollection<Question> Question_AreaTypes { get; set; }
        public ICollection<Question> Question_AuthorTypes { get; set; }

        public ICollection<Question> Question_QuestionRanks { get; set; }

        public ICollection<QuestionJudge> QuestionJudge_QuestionHardnessTypes { get; set; }
        public ICollection<QuestionJudge> QuestionJudge_RepeatnessTypes { get; set; }
        public ICollection<QuestionJudge> QuestionJudge_WhereProblem { get; set; }
        public ICollection<QuestionJudge> QuestionJudge_ReasonProblem { get; set; }
        public ICollection<QuestionAnswerJudge> QuestionAnswerJudge_ReasonProblem { get; set; }
        public ICollection<QuestionJudge> QuestionJudge_QuestionRanks { get; set; }

        public ICollection<Topic> Topic_Hardnesses { get; set; }

        public ICollection<Topic> Topic_AreaTypes { get; set; }

        public ICollection<Lesson> Lesson_Nezams { get; set; }

        public ICollection<EducationTree> EducationTree_States { get; set; }

      

        public ICollection<Media> Media_Type { get; set; }
    }
}
