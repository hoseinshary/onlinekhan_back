using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Publisher
{
    public class PublisherViewModel
    {
        public int Id { get; set; }

        [Display(Name = "انتشارات")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }
    }
}
