using System.Collections.Generic;
using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ViewModels.Student
{
    public class StudentQuestionAssayReportForTopicViewModel
    {
        public List<LessonReportViewModel> LessonReports { get; set; }

        public UserViewModel User { get; set; }
     
    }
}

