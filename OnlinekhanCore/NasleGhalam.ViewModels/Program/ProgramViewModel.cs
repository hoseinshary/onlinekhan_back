using System;
using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Program
{
    public class ProgramViewModel
    {
        [Display(Name = "")]
        public int Id { get; set; }


        [Display(Name = "")]
        public string Name { get; set; }


        [Display(Name = "")]
        public int StudentId { get; set; }


        [Display(Name = "")]
        public bool IsMain { get; set; }


        [Display(Name = "")]
        public DateTime CreatedTime { get; set; }

        public string Description { get; set; }


    }
}
