using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.Common;

namespace NasleGhalam.ViewModels.AssayAnswerSheet
{
    public class AssayAnswerSheetReportViewModel
    {
        [Display(Name = "")]
        public int Id { get; set; }

        public string Title { get; set; }

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

        public decimal ExamScore { get; set; }
        public decimal  CorrectScore { get; set; }

        public decimal WrongScore { get; set; }
        public decimal NonScore { get; set; }


        public IList<int> QuestionIds { get; set; }


    }
}