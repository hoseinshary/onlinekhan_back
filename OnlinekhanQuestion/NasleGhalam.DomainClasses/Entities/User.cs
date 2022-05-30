using System;
using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class User
    {
        public User()
        {
            
            
            QuestionJudges = new HashSet<QuestionJudge>();
            QuestionAnswers = new HashSet<QuestionAnswer>();
            QuestionAnswerJudges = new HashSet<QuestionAnswerJudge>();
            Lessons = new HashSet<Lesson>();
          
            SupervisorQuestions = new HashSet<Question>();
            QuestionsUpdates = new HashSet<QuestionUpdate>();
            Medias = new HashSet<Media>();
            Logs = new HashSet<Log>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string NationalNo { get; set; }

        public bool Gender { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }

        public DateTime LastLogin { get; set; }

        public Role Role { get; set; }

        public int RoleId { get; set; }

        public City City { get; set; }

        public int CityId { get; set; }

        public Student Student { get; set; }

        public string ProfilePic { get; set; }

        public Teacher Teacher { get; set; }

        public  Writer  Writer { get; set; }


        
        public ICollection<Question> SupervisorQuestions { get; set; }
        public ICollection<QuestionJudge> QuestionJudges { get; set; }
        public ICollection<QuestionGroup> QuestionGroups { get; set; }

        public ICollection<QuestionAnswer> QuestionAnswers { get; set; }
        public ICollection<QuestionAnswerJudge> QuestionAnswerJudges { get; set; }
        public ICollection<Lesson> Lessons { get; set; }




        public ICollection<QuestionUpdate> QuestionsUpdates { get; set; }

        public ICollection<Media> Medias { get; set; }
        public ICollection<Log> Logs { get; set; }

    }
}
