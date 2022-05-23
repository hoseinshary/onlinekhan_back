using NasleGhalam.ViewModels._Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels.Ratio;

namespace NasleGhalam.ViewModels.Lesson
{
    public class LessonCreateViewModel
    {
        private int _numberOfJudges;


        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(200, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }

        [Display(Name = "اختصاصی")]
        public bool IsMain { get; set; }

        [Display(Name = "نظام")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_Nezam { get; set; }

        public int NumberOfJudges
        {
            get => _numberOfJudges < 1 ? 1 : _numberOfJudges;
            set => _numberOfJudges = value< 1 ? 1 :value;
        }

        public IEnumerable<RatioCreateViewModel> Ratios { get; set; }

        public IEnumerable<int> EducationTreeIds { get; set; }

        public int LessonDepartmentId { get; set; }
    }
}
