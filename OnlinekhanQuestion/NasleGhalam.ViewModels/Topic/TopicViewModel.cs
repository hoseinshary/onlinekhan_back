using NasleGhalam.ViewModels.Lesson;

namespace NasleGhalam.ViewModels.Topic
{
    public class TopicViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public int ExamStock { get; set; }
        
        public int ExamStockSystem { get; set; }
        
        public short Importance { get; set; }
        
        public bool IsExamSource { get; set; }

        public bool IsActive { get; set; }
        
        public int LookupId_HardnessType { get; set; }
        
        public int LookupId_AreaType { get; set; }
        
        public int LessonId { get; set; }

        public int? ParentTopicId { get; set; }

        public int DisplayPriority { get; set; }

        public LessonViewModel Lesson { get; set; }
    }
}
