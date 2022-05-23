using System;
using System.Collections.Generic;
using System.Linq;
using NasleGhalam.Common;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Assay
    {

        public Assay()
        {
            AssayQuestions = new HashSet<AssayQuestion>();
            AssaySchedules = new HashSet<AssaySchedule>();
            Packages = new HashSet<Package>();
            AssayAnswerSheets = new HashSet<AssayAnswerSheet>();
            Lessons = new HashSet<Lesson>();



        }
        public int Id { get; set; }

        public string Title { get; set; }
        public int Time { get; set; }
        public int LookupId_Importance { get; set; }

        public Lookup Lookup_Importance { get; set; }

        public int LookupId_Type { get; set; }

        public Lookup Lookup_Type { get; set; }

        public int LookupId_QuestionType { get; set; }

        public Lookup Lookup_QuestionType { get; set; }

        public bool IsPublic { get; set; }
        public bool IsOnline { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime DateTimeCreate { get; set; }

        public AssayVarient NumberOfVarient { get; set; }

        public string File1 { get; set; }
        public string File2 { get; set; }
        public string File3 { get; set; }
        public string File4 { get; set; }

        public string QuestionsFile1 { get; set; }

       
        public string QuestionsFile2 { get; set; }
       
        public string QuestionsFile3 { get; set; }
        
        public string QuestionsFile4 { get; set; }
      

        public string QuestionsAnswer1 { get; set; }

       
        public string QuestionsAnswer2 { get; set; }
      
        public string QuestionsAnswer3 { get; set; }
     
        public string QuestionsAnswer4 { get; set; }
      

        public ICollection<Lesson> Lessons { get; set; }

        public ICollection<AssayQuestion> AssayQuestions { get; set; }

        public ICollection<AssayAnswerSheet> AssayAnswerSheets { get; set; }

        public ICollection<AssaySchedule> AssaySchedules { get; set; }

        public ICollection<Package> Packages { get; set; }



    }
}
