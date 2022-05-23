namespace NasleGhalam.DomainClasses.Entities
{
    public class QuestionOption
    {
        public int Id { get; set; }

        public string Context { get; set; }

        public bool IsAnswer { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }
    }
}
