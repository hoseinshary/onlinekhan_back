using NasleGhalam.ViewModels.ProgramItem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Program
{
    public class ProgramCreateViewModel
    {
  


        [Display(Name = "نام برنامه")]
        public string Name { get; set; }


        [Display(Name = "دانش آموز")]
        public int StudentId { get; set; }


        [Display(Name = "توضیحات")]
        public string Description { get; set; }


        public ICollection<ProgramItemCreateViewModel> ProgramItems { get; set; }




    }
}
