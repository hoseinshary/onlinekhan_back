namespace NasleGhalam.DomainClasses.Entities
{
    public class QuestionAnswerView
    {
        public int Id { get; set; }

        public byte Rate { get; set; }

        public int AnswerId { get; set; }

        public QuestionAnswer Answer { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }
    }
}
