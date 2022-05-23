using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.EducationYear
{
    public class EducationYearCreateViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "سال جاری")]
        public bool IsActiveYear { get; set; }
    }
}
