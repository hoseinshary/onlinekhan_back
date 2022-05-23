using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Report
{
    public class AllQuestionOfEachLessonViewModel
    {

        public string  Name { get; set; }

        public int AllQuestionNum { get; set; }

        public int AllQuestionTopiced { get; set; }

        public int AllQuestionJudged { get; set; }

        public int AllQuestionActived { get; set; }

        public int AllQuestionHybrid { get; set; }
        public int AllQuestionJudgedFull { get; set; }


    }
}
