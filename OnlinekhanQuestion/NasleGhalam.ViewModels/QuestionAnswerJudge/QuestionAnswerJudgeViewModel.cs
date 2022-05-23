using NasleGhalam.ViewModels.Lookup;
using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ViewModels.QuestionAnswerJudge
{
    public class QuestionAnswerJudgeViewModel
    {
        public int Id { get; set; }

        public bool IsActiveQuestionAnswer { get; set; }

        public int QuestionAnswerId { get; set; }

        
        public bool IsMaster { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public int LookupId_ReasonProblem { get; set; }

        public UserViewModel User { get; set; }

        public LookupViewModel Lookup_ReasonProblem { get; set; }
    }
}
