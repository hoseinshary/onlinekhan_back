using System.ComponentModel.DataAnnotations;
using NasleGhalam.Common;
using NasleGhalam.ViewModels._Attributes;

namespace NasleGhalam.ViewModels.AxillaryBook
{
    public class AxillaryBookCreateViewModel
    {
        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }

        [Display(Name = "سال انتشار")]
        [Range(1350, 1500, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Range")]
        public short PublishYear { get; set; }

        [Display(Name = "نویسنده")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(100, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Author { get; set; }

        [Display(Name = "شابک")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(100, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Isbn { get; set; }

        [Display(Name = "قلم")]
        public string Font { get; set; }

        [Display(Name = "نوع چاپ")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_PrintType { get; set; }

        public string ImgName{ get; set; }

        public string ImgAbsPath => $"{SitePath.AxillaryBookRelPath.ToAbsolutePath()}{ImgName}";

        [Display(Name = "قیمت")]
        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Range")]
        public int Price { get; set; }

        [Display(Name = "قیمت پشت جلد")]
        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Range")]
        public int OriginalPrice { get; set; }

        [Display(Name = "نوع قطع")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_BookType { get; set; }

        [Display(Name = "نوع کاغذ")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_PaperType { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(300, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Description { get; set; }

        [Display(Name = "انتشارات")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int PublisherId { get; set; }
    }
}
