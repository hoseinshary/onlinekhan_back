using System;
using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class QuestionGroup
    {
        
        public QuestionGroup()
        {
            
            Questions = new HashSet<Question>();
        }
        public int Id { get; set; }

        public string Title { get; set; }





        public DateTime InsertTime { get; set; }

        public string File { get; set; }
        
        public int LessonId { get; set; }

        public Lesson Lesson { get; set; }

        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
        public User User { get; set; }


        public ICollection<Question> Questions { get; set; }

    }
}
