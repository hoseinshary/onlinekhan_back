using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.EducationYear
{
    public class EducationYearUpdateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "سال جاری")]
        public bool IsActiveYear { get; set; }
    }
}
