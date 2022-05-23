using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ViewModels.Teacher
{
    public class TeacherUpdateViewModel
    {
        public int Id { get; set; }


       // [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        [Display(Name = "نام پدر")]
        public string FatherName { get; set; }


       // [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(300, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        [Display(Name = "آدرس")]
        public string Address { get; set; }

        public UserUpdateViewModel User { get; set; }
    }
}
