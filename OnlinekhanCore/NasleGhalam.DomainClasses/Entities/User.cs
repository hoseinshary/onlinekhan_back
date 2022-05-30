using System;
using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class User
    {
        public User()
        {
            
            
            QuestionAnswers = new HashSet<QuestionAnswer>();
            Lessons = new HashSet<Lesson>();
            AssayAnswerSheets = new HashSet<AssayAnswerSheet>();
            Assays = new HashSet<Assay>();
            Sales = new HashSet<Sale>();
            SupervisorQuestions = new HashSet<Question>();
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

    

        public Student Student { get; set; }

        public string ProfilePic { get; set; }

        public Teacher Teacher { get; set; }

        public  Writer  Writer { get; set; }


        
        public ICollection<Question> SupervisorQuestions { get; set; }
      

        public ICollection<QuestionAnswer> QuestionAnswers { get; set; }
     
        public ICollection<Lesson> Lessons { get; set; }

        public ICollection<AssayAnswerSheet> AssayAnswerSheets { get; set; }

        public ICollection<Assay> Assays { get; set; }

        public ICollection<Sale> Sales { get; set; }


    

        public ICollection<Media> Medias { get; set; }
        public ICollection<Log> Logs { get; set; }


    }
}
