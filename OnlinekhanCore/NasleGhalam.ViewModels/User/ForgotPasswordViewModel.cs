using NasleGhalam.ViewModels._Attributes;
using System.ComponentModel.DataAnnotations;
namespace NasleGhalam.ViewModels.User
{
    public class ForgotPasswordViewModel
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [StringLength(maximumLength: 11, MinimumLength = 11, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Length")]
        [RegularExpression("\\d+", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Numeric")]
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        [MinLength(5, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MinLen")]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }



        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [Display(Name = "کد احراز هویت")]
        public string VerificationCode { get; set; }
    }
}
