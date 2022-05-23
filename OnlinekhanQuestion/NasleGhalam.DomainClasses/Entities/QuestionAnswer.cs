using System.Collections.Generic;
using NasleGhalam.Common;

namespace NasleGhalam.DomainClasses.Entities
{
    public class QuestionAnswer
    {
        public QuestionAnswer()
        {
            QuestionAnswerViews = new HashSet<QuestionAnswerView>();
            QuestionAnswerJudges = new HashSet<QuestionAnswerJudge>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Context { get; set; }

        public string FilePath { get; set; }

        public int LookupId_AnswerType { get; set; }
        public Lookup Lookup_AnswerType { get; set; }

        public string LessonName { get; set; }

        public string Description { get; set; }

        public int WriterId { get; set; }

        public Writer Writer { get; set; }

        public bool IsMaster { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }
        public bool IsUpdate { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }

        public QuestionAnswerType QuestionAnswerType { get; set; }

        public string QuestionAnswerTypeDisplay => QuestionAnswerType.GetDisplayName();


        public ICollection<QuestionAnswerView> QuestionAnswerViews { get; set; }
        public ICollection<QuestionAnswerJudge> QuestionAnswerJudges { get; set; }
    }
}
