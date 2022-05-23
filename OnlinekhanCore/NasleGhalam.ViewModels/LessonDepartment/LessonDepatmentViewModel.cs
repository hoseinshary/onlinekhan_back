using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.LessonDepartment
{
    public class LessonDepartmentViewModel
    {
        [Display(Name = "")]
        public int Id { get; set; }


        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }


    }
}
