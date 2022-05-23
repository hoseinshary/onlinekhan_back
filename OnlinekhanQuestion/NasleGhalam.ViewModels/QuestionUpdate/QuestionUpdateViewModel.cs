using System;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.Common;

namespace NasleGhalam.ViewModels.QuestionUpdate
{
    public class QuestionUpdateViewModel
    {
        [Display(Name = "")]
        public int Id { get; set; }


        [Display(Name = "کاربر")]
        public int UserId { get; set; }


        [Display(Name = "سوال")]
        public int QuestionId { get; set; }


        [Display(Name = "تاریخ")]
        public DateTime DateTime { get; set; }


        [Display(Name = "توضیجات")]
        public string Description { get; set; }


        [Display(Name = "")]
        public QuestionActivity QuestionActivity { get; set; }


    }
}