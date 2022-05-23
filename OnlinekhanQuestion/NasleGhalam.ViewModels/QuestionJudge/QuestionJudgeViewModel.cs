using NasleGhalam.ViewModels.Lookup;
using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ViewModels.QuestionJudge
{
    public class QuestionJudgeViewModel
    {
        public int Id { get; set; }

        public bool IsStandard { get; set; }

        public bool IsDelete { get; set; }

        public bool IsUpdate { get; set; }

        public bool IsLearning { get; set; }

        public string IsStandardName => IsStandard ? "بلی" : "خیر";

        public string IsDeleteName => IsDelete ? "بلی" : "خیر";

        public string IsUpdateName => IsUpdate ? "بلی" : "خیر";

        public string IsLearningName => IsLearning ? "بلی" : "خیر";

        public short ResponseSecond { get; set; }

        public bool IsActiveQuestion { get; set; }

        public bool IsActiveQuestionAnswer { get; set; }

        public string IsActiveQuestionName => IsActiveQuestion ? "بلی" : "خیر";

        public string IsActiveQuestionAnswerName => IsActiveQuestionAnswer ? "بلی" : "خیر";

        public int LookupId_QuestionHardnessType { get; set; }

        public int LookupId_RepeatnessType { get; set; }
        
        public int LookupId_QuestionRank { get; set; }


        public int QuestionId { get; set; }

        public LookupViewModel Lookup_QuestionHardnessType { get; set; }

        public LookupViewModel Lookup_RepeatnessType { get; set; }

        public LookupViewModel Lookup_WhereProblem { get; set; }
        public LookupViewModel Lookup_ReasonProblem { get; set; }

        public UserViewModel User { get; set; }

        public int LookupId_WhereProblem { get; set; }

        
        public int LookupId_ReasonProblem { get; set; }

        
        public string EducationGroup { get; set; }

        public string Description { get; set; }

    }
}
