using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Writer
    {
        public Writer()
        {
            Questions = new HashSet<Question>();
            QuestionAnswers = new HashSet<QuestionAnswer>();
            Medias = new HashSet<Media>();
        }

        public int Id { get; set; }

        public string Name { get; set; }


        public string ProfilePic { get; set; }

       // public int? User_Id { get; set; }

        public User User { get; set; }

        public ICollection<QuestionAnswer> QuestionAnswers { get; set; }
        public ICollection<Question> Questions { get; set; }

        public ICollection<Media> Medias { get; set; }
    }
}
