using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ViewModels.Student
{
    public class StudentQuestionAssayReportViewModel
    {

        public int Id { get; set; }

        public int NumberOfNewQuestions { get; set; }
        public int NumberOfAssayQuestions { get; set; }
        public int NumberOfHomeworkQuestions { get; set; }

        public UserViewModel User { get; set; }
     
    }
}

