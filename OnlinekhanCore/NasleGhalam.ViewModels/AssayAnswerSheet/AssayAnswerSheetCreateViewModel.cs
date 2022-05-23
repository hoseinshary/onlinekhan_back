using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.Common;

namespace NasleGhalam.ViewModels.AssayAnswerSheet
{
    public class AssayAnswerSheetCreateViewModel
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

        public IList<int> Answers { get; set; }

        public IList<bool> MaybeList { get; set; }

        public IList<bool> AfterList { get; set; }
        public IList<bool> CantList { get; set; }


    }
}