using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Topic
    {
        
        public Topic()
        {
            ChildrenTopic = new HashSet<Topic>();
            EducationBooks = new HashSet<EducationBook>();
            Questions = new HashSet<Question>();
            Medias = new HashSet<Media>();
        }
        public int Id { get; set; }

        public string Title { get; set; }

        public int ExamStock { get; set; }

        public int ExamStockSystem { get; set; }

        public short Importance { get; set; }

        public bool IsExamSource { get; set; }

        public int LookupId_HardnessType { get; set; }
        public Lookup Lookup_HardnessType { get; set; }

        public int LookupId_AreaType { get; set; }
        public Lookup Lookup_AreaType { get; set; }

        public bool IsActive { get; set; }

        public int? ParentTopicId { get; set; }

        public Topic ParentTopic { get; set; }

        public ICollection<Topic> ChildrenTopic { get; set; }

        public int LessonId { get; set; }

        public Lesson Lesson { get; set; }

        public ICollection<EducationBook> EducationBooks { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<Media> Medias { get; set; }

        public int DisplayPriority { get; set; }

    }
}
