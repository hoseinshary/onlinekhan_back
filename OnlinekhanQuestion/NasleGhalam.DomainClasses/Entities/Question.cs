using System;
using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Question
    {
        public Question()
        {
            QuestionJudges = new HashSet<QuestionJudge>();
            QuestionOptions = new HashSet<QuestionOption>();
            QuestionEquals1 = new HashSet<QuestionEqual>();
            QuestionEquals2 = new HashSet<QuestionEqual>();
            Tags = new HashSet<Tag>();
            Boxes = new HashSet<Box>();
            QuestionAnswers = new HashSet<QuestionAnswer>();
            Topics = new HashSet<Topic>();
            AssayQuestions =new HashSet<AssayQuestion>();
            QuestionUpdates = new HashSet<QuestionUpdate>();
            Supervisors = new HashSet<User>();
            Lookup_AreaTypes =  new HashSet<Lookup>();

        }
        public int Id { get; set; }

        public string Context { get; set; }

        public int QuestionNumber { get; set; }

        public int LookupId_QuestionType { get; set; }
        public Lookup Lookup_QuestionType { get; set; }

        public int QuestionPoint { get; set; }

        public int LookupId_QuestionHardnessType { get; set; }
        public Lookup Lookup_QuestionHardnessType { get; set; }

        public int LookupId_RepeatnessType { get; set; }
        public Lookup Lookup_RepeatnessType { get; set; }

        public bool UseEvaluation { get; set; }

        public bool IsStandard { get; set; }

        public int LookupId_AuthorType { get; set; }

        public Lookup Lookup_AuthorType { get; set; }

        public int WriterId { get; set; }

        public Writer Writer { get; set; }
        public short ResponseSecond { get; set; }
        
        public ICollection<Lookup> Lookup_AreaTypes { get; set; }

        public int LookupId_QuestionRank { get; set; }
        public Lookup Lookup_QuestionRank { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public DateTime InsertDateTime { get; set; }

  

        public bool IsActive { get; set; }

        public bool IsExercise { get; set; }

        public bool IsLearning { get; set; }
        public bool IsDelete { get; set; }
        public bool IsUpdate { get; set; }

        public bool IsHybrid { get; set; }
        public int AnswerNumber { get; set; }

        public string  TopicAnswer { get; set; }
        public bool Deleted { get; set; }
        public ICollection<QuestionJudge> QuestionJudges { get; set; }
               
        public ICollection<QuestionOption> QuestionOptions { get; set; }
               
        public ICollection<QuestionEqual> QuestionEquals1 { get; set; }
               
        public ICollection<QuestionEqual> QuestionEquals2 { get; set; }
               
        public ICollection<Tag> Tags { get; set; }

        //update users
        public ICollection<QuestionUpdate> QuestionUpdates { get; set; }

        public ICollection<User> Supervisors { get; set; }

        public ICollection<Box> Boxes { get; set; }
     
        public ICollection<QuestionAnswer> QuestionAnswers { get; set; }

        public ICollection<Topic> Topics { get; set; }
        public ICollection<QuestionGroup> QuestionGroups { get; set; }

        public ICollection<AssayQuestion> AssayQuestions { get; set; }

        
    }
}
