using NasleGhalam.ViewModels._Attributes;
using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.UniversityBranch
{
    public class UniversityBranchViewModel
    {
        public int Id { get; set; }


        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }


        [Display(Name = "معدل سایت")]
        public int SiteAverage { get; set; }


        [Display(Name = "کمینه تراز 1")]
        public int Balance1Low { get; set; }

        [Display(Name = "بیشینه تراز 1")]
        public int Balance1High { get; set; }

        [Display(Name = "کمینه تراز 2")]
        public int Balance2Low { get; set; }
        [Display(Name = "بیشینه تراز 2")]
        public int Balance2High { get; set; }


        [Display(Name = "زیرگروه درسی")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int EducationSubGroupId { get; set; }

        public string EducationSubGroupName { get; set; }


    }
}
