namespace NasleGhalam.DomainClasses.Entities
{
    public class QuestionAnswerJudge
    {
        public QuestionAnswerJudge()
        {
        }
        public int Id { get; set; }

        public bool IsActiveQuestionAnswer { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }

        public int QuestionAnswerId { get; set; }

        public QuestionAnswer QuestionAnswer { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public bool IsMaster { get; set; }

        public int LookupId_ReasonProblem { get; set; }
        public Lookup Lookup_ReasonProblem { get; set; }
        

        public string Description { get; set; }


    }
}
