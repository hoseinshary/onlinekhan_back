using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels._Attributes;

namespace NasleGhalam.ViewModels.EducationSubGroup
{
    public class EducationSubGroupUpdateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        [Display(Name = "درخت آموزش")]
        public int EducationTreeId { get; set; }
    }
}
