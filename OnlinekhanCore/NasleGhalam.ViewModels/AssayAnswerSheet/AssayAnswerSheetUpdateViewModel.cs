using System;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.Common;

namespace NasleGhalam.ViewModels.AssayAnswerSheet
{
    public class AssayAnswerSheetUpdateViewModel
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


    }
}