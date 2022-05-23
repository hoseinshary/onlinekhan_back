using System;
using System.Collections.Generic;
using NasleGhalam.Common;

namespace NasleGhalam.DomainClasses.Entities
{
    public class AssayAnswerSheet
    {
        public int Id { get; set; }

      

        public int AssayId { get; set; }

        public Assay Assay { get; set; }



        public int UserId { get; set; }

        public User User { get; set; }

        public AssayVarient AssayVarient { get; set; }

        public DateTime AssayTime { get; set; }

        public DateTime DateTime { get; set; }

        public string AnswerTimes { get; set; }

        public string Answers { get; set; }


        public string MaybeList { get; set; }

        public string AfterList { get; set; }
        public string CantList { get; set; }

    }
}
