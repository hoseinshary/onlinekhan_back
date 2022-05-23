using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NasleGhalam.Common;
using NasleGhalam.ViewModels.City;

namespace NasleGhalam.ViewModels.Resume
{
    public class ResumeViewModel
    {
        public ResumeViewModel()
        {
            Publications = new List<PublicationViewModel>();
            EducationCertificates = new List<EducationCertificateViewModel>();
            TeachingResumes = new List<TeachingResumeViewModel>();
        }

        public int Id { get; set; }


        [Display(Name = "شعبه")]
        public string Branch { get; set; }


        [Display(Name = "زمان ثبت")]
        public DateTime CreationDateTime { get; set; }

        public string PCreationDateTime => CreationDateTime.ToPersianDateTime();


        [Display(Name = "نام")]
        public string Name { get; set; }


        [Display(Name = "نام خانوادگی")]
        public string Family { get; set; }


        [Display(Name = "نام پدر")]
        public string FatherName { get; set; }


        [Display(Name = "شماره شناسنامه")]
        public string IdNumber { get; set; }


        [Display(Name = "کد ملی")]
        public string NationalNo { get; set; }


        [Display(Name = "جنسیت")]
        public bool Gender { get; set; }

        public string GenderName => Gender ? "مرد" : "زن";


        [Display(Name = "تلفن")]
        public string Phone { get; set; }


        [Display(Name = "موبایل")]
        public string Mobile { get; set; }


        [Display(Name = "صادره")]
        public string CityBorn { get; set; }


        [Display(Name = "تاریخ تولد")]
        public DateTime Birthday { get; set; }

        public string PBirthday => Birthday.ToPersianDate();


        [Display(Name = "وضعیت تاهل")]
        public bool Marriage { get; set; }


        [Display(Name = "مذهب")]
        public string Religion { get; set; }


        [Display(Name = "آدرس")]
        public string Address { get; set; }


        [Display(Name = "شهر")]
        public int CityId { get; set; }


        [Display(Name = "کد پستی")]
        public string PostCode { get; set; }


        [Display(Name = "شغل پدر")]
        public string FatherJob { get; set; }


        [Display(Name = "مدرک پدر")]
        public Degree FatherDegree { get; set; }


        [Display(Name = "شماره تماس پدر")]
        public string FatherPhone { get; set; }


        [Display(Name = "شغل مادر")]
        public string MotherJob { get; set; }


        [Display(Name = "مدرک مادر")]
        public Degree MotherDegree { get; set; }


        [Display(Name = "شماره تماس مادر")]
        public string MotherPhone { get; set; }


        [Display(Name = "شغل همسر")]
        public string PartnerJob { get; set; }


        [Display(Name = "مدرک همسر")]
        public Degree PartnerDegree { get; set; }


        [Display(Name = "شماره تماس همسر")]
        public string PartnerPhone { get; set; }


        [Display(Name = "معرف 1")]
        public string Reagent1 { get; set; }


        [Display(Name = "نسبت معرف 1")]
        public string RelationReagent1 { get; set; }


        [Display(Name = "شعل معرف 1")]
        public string JobReagent1 { get; set; }


        [Display(Name = "شماره تماس معرف 1")]
        public string PhoneReagent1 { get; set; }


        [Display(Name = "آدرس معرف 1")]
        public string AddressReagent1 { get; set; }


        [Display(Name = "معرف 2")]
        public string Reagent2 { get; set; }


        [Display(Name = "نسبت معرف 2")]
        public string RelationReagent2 { get; set; }


        [Display(Name = "شغل معرف 2")]
        public string JobReagent2 { get; set; }


        [Display(Name = "شماره تماس معرف 2")]
        public string PhoneReagent2 { get; set; }


        [Display(Name = "آدرس معرف 2")]
        public string AddressReagent2 { get; set; }


        [Display(Name = "گزینش آموزش و پرورش دارد /ندارد")]
        public bool HaveEducationCertificate { get; set; }


        [Display(Name = "گزینش از ارگان دیگر ")]
        public bool HaveAnotherCertificate { get; set; }


        [Display(Name = "نام ارگان")]
        public string AnotherCertificate { get; set; }


        [Display(Name = "سابقه تالیف")]
        public bool HavePublication { get; set; }


        [Display(Name = "تعداد تالیفات")]
        public int NumberOfPublication { get; set; }


        [Display(Name = "سابقه تدریس")]
        public bool HaveTeachingResume { get; set; }


        [Display(Name = "تعداد سال سابقه تدریس")]
        public int NumberOfTeachingYear { get; set; }


        [Display(Name = "تقاضای تدریس 1")]
        public bool TeachingRequest1 { get; set; }
        [Display(Name = "تقاضای  تالیف 1")]
        public bool PublishingRequest1 { get; set; }


        [Display(Name = "پایه تقاضای 1")]
        public Maghta MaghtaRequest1 { get; set; }


        [Display(Name = "نوع تقاضای 1")]
        public KindRequest KindRequest1 { get; set; }


        [Display(Name = "درس تقاضای 1")]
        public string LessonNameRequest1 { get; set; }


        [Display(Name = "تقاضای تدریس تقاضای 2")]
        public bool TeachingRequest2 { get; set; }
        [Display(Name = "تقاضای تالیف تقاضای 2")]
        public bool PublishingRequest2 { get; set; }

        [Display(Name = "پایه تقاضای 2")]
        public Maghta MaghtaRequest2 { get; set; }


        [Display(Name = "نوع تقاضای 2")]
        public KindRequest KindRequest2 { get; set; }


        [Display(Name = "درس تقاضای 2")]
        public string LessonNameRequest2 { get; set; }


        [Display(Name = "درخواست برای مشاوره")]
        public bool RequestForAdvice { get; set; }

        [Display(Name = "پایه مشاوره")]
        public Maghta MaghtaAdvice { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "سابقه تالیف")]
        public List<PublicationViewModel> Publications { get; set; }

        [Display(Name = "مدارک تحصیلی")]
        public List<EducationCertificateViewModel> EducationCertificates { get; set; }

        [Display(Name = "سابقه تدریس")]
        public List<TeachingResumeViewModel> TeachingResumes { get; set; }

        public CityViewModel City { get; set; }

        public int ProvinceId { get; set; }

        public string LastEducationCertificate
        {
            get
            {
                var education = EducationCertificates.OrderByDescending(x => x.Year).FirstOrDefault();
                return education != null ? $"{education.Subject} - {education.DegreeCertificateName}" : "";
            }
        }
    }
}
