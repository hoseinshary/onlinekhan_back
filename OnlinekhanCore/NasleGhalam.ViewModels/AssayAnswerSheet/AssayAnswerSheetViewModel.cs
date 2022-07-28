using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.Common;
using NasleGhalam.ViewModels.Assay;

namespace NasleGhalam.ViewModels.AssayAnswerSheet
{
    public class AssayAnswerSheetViewModel
    {
        [Display(Name = "")]
        public int Id { get; set; }


        [Display(Name = "")]
        public int AssayId { get; set; }


        [Display(Name = "")]
        public int UserId { get; set; }


        [Display(Name = "")]
        public AssayVarient AssayVarient { get; set; }


        [Display(Name = "")]
        public DateTime AssayTime { get; set; }


        [Display(Name = "")]
        public DateTime DateTime { get; set; }

        public IList<int> AnswerTimes { get; set; }

        public IList<string> Answers { get; set; }

        public IList<bool> MaybeList { get; set; }

        public IList<bool> AfterList { get; set; }
        public IList<bool> CantList { get; set; }

        public IList<int> QuestionIds { get; set; }

        public AssayViewModel Assay { get; set; }
        public IList<AssayAnswerSheetCorectExamViewModel> AnswerSheetCorectExams { get; set; }

        public IList<QuestionTotal> TotalAnswer { get; set; }
       
    }
    public class QuestionTotal
    {
        public int QuestionId { get; set; }
        public int TotalFirstChoice { get; set; }
        public int TotalSecondChoice { get; set; }
        public int TotalThirdChoice { get; set; }
        public int TotalForthChoice { get; set; }
        public int TotalNonChoice { get; set; }

    }
}