using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.Common;
using NasleGhalam.ViewModels.Topic;

namespace NasleGhalam.ViewModels.Media
{
    public class MediaViewModel
    {
        [Display(Name = "")]
        public int Id { get; set; }

        public int Code => Id;

        [Display(Name = "")]
        public string Title { get; set; }


        [Display(Name = "")]
        public int LookupId_MediaType { get; set; }


        [Display(Name = "")]
        public int WriterId { get; set; }


        [Display(Name = "")]
        public string Description { get; set; }


        [Display(Name = "")]
        public string FileName { get; set; }

        public string MediaPath => $"/Api/Media/GetFile?id={FileName}".ToFullRelativePath();

        [Display(Name = "")]
        public int Price { get; set; }


        [Display(Name = "")]
        public DateTime InsertDateTime { get; set; }


        [Display(Name = "")]
        public int UserId { get; set; }


        [Display(Name = "")]
        public bool IsActive { get; set; }


        public string CoverImage { get; set; }

        public string CoverImagePath => $"/Api/Media/GetFile?id={CoverImage}".ToFullRelativePath();


        public string Length { get; set; }

        public int YearOfBook { get; set; }

        public string PagesOfBook { get; set; }

        public List<TopicViewModel> Topics { get; set; } = new List<TopicViewModel>();


    }
}
