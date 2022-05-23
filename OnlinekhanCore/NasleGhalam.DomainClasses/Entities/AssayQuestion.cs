using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class AssayQuestion
    {

        public int Id { get; set; }

        public int AssayId { get; set; }

        public Assay Assay { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }


        public int  LessonId { get; set; }


      
        

    }
}
