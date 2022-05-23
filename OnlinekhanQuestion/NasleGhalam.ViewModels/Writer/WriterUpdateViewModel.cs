using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Writer
{
    public class WriterUpdateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "کاربر")]
        public int? UserId { get; set; }
        public string ProfilePic { get; set; }
    }
}
