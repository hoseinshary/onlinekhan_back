using System.Collections.Generic;
using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ViewModels.Student
{
    public class TopicReportViewModel
    {
        public int  ID { get; set; }

        public int NumberOfNewQuestions { get; set; }
        public int NumberOfAssayQuestions { get; set; }
        public int NumberOfHomeworkQuestions { get; set; }

    }
}

