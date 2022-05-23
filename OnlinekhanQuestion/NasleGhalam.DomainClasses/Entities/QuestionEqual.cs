using NasleGhalam.Common;

namespace NasleGhalam.DomainClasses.Entities
{
    public class QuestionEqual
    {
        public QuestionEqual()
        {
        }
        public int Id { get; set; }

        public EqualType EqualType { get; set; }

        public int QuestionId1 { get; set; }

        public Question Question1 { get; set; }

        public int QuestionId2 { get; set; }

        public Question Question2 { get; set; }
    }
}
