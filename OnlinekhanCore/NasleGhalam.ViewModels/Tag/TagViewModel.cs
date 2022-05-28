using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Tag
{
    public class TagViewModel
    {
        public int Id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }

        public bool IsSource { get; set; }

        public string IsSourceStr => IsSource ? "بلی" : "خیر";
    }
}
