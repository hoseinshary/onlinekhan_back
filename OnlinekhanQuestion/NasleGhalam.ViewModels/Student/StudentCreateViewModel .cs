﻿using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ViewModels.Student
{
    public class StudentCreateViewModel
    {
        public int Id { get; set; }


     //   [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        [Display(Name = "نام پدر")]
        public string FatherName { get; set; }


        //[Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(300, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        [Display(Name = "آدرس")]
        public string Address { get; set; }

        public UserCreateViewModel User { get; set; }
    }
}
