using System;
using NasleGhalam.Common;

namespace NasleGhalam.ViewModels.QuestionGroup
{
    public class QuestionGroupViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime InsertTime { get; set; }

        public string PInsertTime => InsertTime.ToPersianDateTime();

        public string File { get; set; }

        public int LessonId { get; set; }

        public int UserId { get; set; }

        public string QuestionGroupWordPath => $"/Api/QuestionGroup/GetWordFile/{File}".ToFullRelativePath();

        public string QuestionGroupExcelPath => $"/Api/QuestionGroup/GetExcelFile/{File}".ToFullRelativePath();
    }
}
