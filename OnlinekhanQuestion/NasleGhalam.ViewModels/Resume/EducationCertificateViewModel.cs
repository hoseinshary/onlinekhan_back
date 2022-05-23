using System.ComponentModel.DataAnnotations;
using NasleGhalam.Common;

namespace NasleGhalam.ViewModels.Resume
{
    public class EducationCertificateViewModel
    {

        [Display(Name = "مدرک")]
        public DegreeCertificate DegreeCertificate { get; set; }

        public string DegreeCertificateName => DegreeCertificate.GetDisplayName();

        [Display(Name = "رشته")]
        public string Subject { get; set; }

        [Display(Name = "مرکز تحصیلی")]
        public string EducationCenterName { get; set; }

        [Display(Name = "شهر")]
        public string CityName { get; set; }

        [Display(Name = "نوع")]
        public TypeEducationCenter TypeEducationCenter { get; set; }


        [Display(Name = "سال")]
        public string Year { get; set; }

        [Display(Name = "معدل")]
        public float Score { get; set; }
    }
}
