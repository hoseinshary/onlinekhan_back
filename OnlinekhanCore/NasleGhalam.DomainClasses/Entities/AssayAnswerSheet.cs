using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [MaxLength()]
        public string AnswerTimes { get; set; }
        [MaxLength()]
        public string Answers { get; set; }

        [MaxLength()]
        public string MaybeList { get; set; }
        [MaxLength()]
        public string AfterList { get; set; }
        [MaxLength()]
        public string CantList { get; set; }

    }
}
