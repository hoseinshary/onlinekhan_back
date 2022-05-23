using System.Collections.Generic;
using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ViewModels.Student
{
    public class LessonReportViewModel
    {
        public int Id { get; set; }

        public List<TopicReportViewModel> TopicReports { get; set; } 
     
    }
}

