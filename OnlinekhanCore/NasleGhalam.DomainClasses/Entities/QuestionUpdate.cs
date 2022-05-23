using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using NasleGhalam.Common;


namespace NasleGhalam.DomainClasses.Entities
{
    public class QuestionUpdate
    {
        

        public int Id { get; set; }

        public int UserId { get; set; }

        public int QuestionId { get; set; }

        public DateTime DateTime { get; set; }

        public string Description { get; set; }

        public QuestionActivity QuestionActivity { get; set; }

        public User User { get; set; }

        public Question Question { get; set; }



        
    }
}
