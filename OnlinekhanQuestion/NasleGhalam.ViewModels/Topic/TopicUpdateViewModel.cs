using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels._Attributes;

namespace NasleGhalam.ViewModels.Topic
{
    public class TopicUpdateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(200, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Title { get; set; }

        [Display(Name = "سهمیه در کنکور")]
        public int ExamStock { get; set; }

        [Display(Name = "سهمیه در کنکور سیستمی")]
        public int ExamStockSystem { get; set; }

        [Display(Name = "ضریب اهمیت")]
        public short Importance { get; set; }

        [Display(Name = "سهمیه در کنکور سیستمی")]
        public bool IsExamSource { get; set; }

        [Display(Name = "درجه سختی")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_HardnessType { get; set; }

        [Display(Name = "حیطه مبحث")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_AreaType { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        [Display(Name = "درس")]
        public int LessonId { get; set; }

        [Display(Name = "مبحث پدر")]
        public int? ParentTopicId { get; set; }


        public int DisplayPriority { get; set; }
    }
}
