using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Report
{
    public class AllUsersReporQuestionViewModel
    {

        public string  Name { get; set; }

        public string Family { get; set; }

        public int NumberOfQuestionJudged { get; set; }

        public int NumberOfQuestionAnswerJudged { get; set; }

        public int NumberOfWriteQuestion { get; set; }

        public int NumberOfSupervisorQuestion { get; set; }

        public int NumberOfQuestionTopiced { get; set; }
        public string Department { get; set; }


    }
}
