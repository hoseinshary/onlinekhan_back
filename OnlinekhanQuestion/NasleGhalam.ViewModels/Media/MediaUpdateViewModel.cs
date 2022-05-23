using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Media
{
    public class MediaUpdateViewModel
    {
        [Display(Name = "آی دی")]
        public int Id { get; set; }


        [Display(Name = "عنوان")]
        public string Title { get; set; }


        [Display(Name = "نوع")]
        public int LookupId_MediaType { get; set; }


        [Display(Name = "نویسنده")]
        public int WriterId { get; set; }


        [Display(Name = "توضیحات")]
        public string Description { get; set; }


        [Display(Name = "نام فایل")]
        public string FileName { get; set; }


        [Display(Name = "قیمت")]
        public int Price { get; set; }


        [Display(Name = "تاریخ ثبت")]
        public DateTime InsertDateTime { get; set; }


        [Display(Name = "کاربر")]
        public int UserId { get; set; }


        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        public string CoverImage { get; set; }

        public string Length { get; set; }

        public int YearOfBook { get; set; }

        public string PagesOfBook { get; set; }

        public List<int> TopicIds { get; set; } = new List<int>();
    }
}
