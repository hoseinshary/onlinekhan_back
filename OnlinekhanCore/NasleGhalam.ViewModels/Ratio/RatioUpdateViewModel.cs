using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels._Attributes;

namespace NasleGhalam.ViewModels.Ratio
{
    public class RatioUpdateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "ضریب")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [Range(0, byte.MaxValue, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Range")]
        public byte Rate { get; set; }

        [Display(Name = "درس")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LessonId { get; set; }

        [Display(Name = "زیر گروه درسی")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int EducationSubGroupId { get; set; }
    }
}
