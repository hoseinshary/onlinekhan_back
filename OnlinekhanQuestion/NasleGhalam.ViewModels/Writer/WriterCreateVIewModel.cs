using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Writer
{
    public class WriterCreateViewModel
    {
        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }

        [Display(Name = "کاربر")]
        public int UserId { get; set; }
        public string ProfilePic { get; set; }
    }
}
