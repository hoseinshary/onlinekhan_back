using System;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.Common;
using NasleGhalam.ViewModels._Attributes;

namespace NasleGhalam.ViewModels.User
{
    public class UserCreateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        [Display(Name = "نام خانوادگی")]
        public string Family { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        [MinLength(5, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MinLen")]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [StringLength(maximumLength: 11, MinimumLength = 10, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RangeLength")]
        [NationalNo(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "NationalNo")]
        [RegularExpression("\\d+", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Numeric")]
        [Display(Name = "کد ملی")]
        public string NationalNo { get; set; }

        [Display(Name = "جنسیت")]
        public bool Gender { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Length")]
        [RegularExpression("\\d+", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Numeric")]
        [Display(Name = "تلفن ثابت")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [StringLength(maximumLength: 11, MinimumLength = 11, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Length")]
        [RegularExpression("\\d+", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Numeric")]
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }


        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        [Display(Name = "نقش")]
        public int RoleId { get; set; }


        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        [Display(Name = "شهر")]
        public int CityId { get; set; }

        [Display(Name = "عکس کاربری")]
        public string ProfilePic { get; set; }


        [Display(Name = "فایل")]
        public string Base64File { get; set; }

        public byte[] FileBytes
        {
            get
            {
                try
                {
                    if (!string.IsNullOrEmpty(Base64File))
                    {
                        return Convert.FromBase64String(Base64File.Replace(
                            Base64File.Substring(0, Base64File.IndexOf("base64,", StringComparison.Ordinal) + 7), ""));
                    }
                }
                catch
                {
                    // ignored
                }

                return new byte[] { };
            }
        }


        public string IntroducedCode { get; set; }

        public Field EducationGroupEnum { get; set; }

        public Maghta GradeEnum { get; set; }

        public string SchoolName { get; set; }
    }
}
