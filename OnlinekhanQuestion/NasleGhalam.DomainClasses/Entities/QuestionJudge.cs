namespace NasleGhalam.DomainClasses.Entities
{
    public class QuestionJudge
    {
        public QuestionJudge()
        {
        }
        public int Id { get; set; }

        public int LookupId_QuestionHardnessType { get; set; }
        public Lookup Lookup_QuestionHardnessType { get; set; }

        public int LookupId_RepeatnessType { get; set; }
        public Lookup Lookup_RepeatnessType { get; set; }


        public bool IsStandard { get; set; }

        public bool IsDelete { get; set; }

        public bool IsUpdate { get; set; }

        public bool IsLearning { get; set; }
        public bool IsActiveQuestion { get; set; }

        public bool IsActiveQuestionAnswer { get; set; }


        public short ResponseSecond { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }


        public int LookupId_WhereProblem { get; set; }
        public Lookup Lookup_WhereProblem { get; set; }

        public int LookupId_ReasonProblem { get; set; }
        public Lookup Lookup_ReasonProblem { get; set; }

        public int LookupId_QuestionRank { get; set; }
        public Lookup Lookup_QuestionRank { get; set; }

        public string EducationGroup { get; set; }

        public string Description { get; set; }


    }
}
