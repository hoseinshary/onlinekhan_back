using System;
using System.Collections.Generic;
using NasleGhalam.Common;
using NasleGhalam.ViewModels.Lesson;

namespace NasleGhalam.ViewModels.Package
{
    public class PackageViewModel
    {
        public int Id { get; set; }


        public string Name { get; set; }


        public string ImageFile { get; set; }


        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }


        public int Price { get; set; }


        public int TimeDays { get; set; }


        public DateTime CreateDateTime { get; set; }


        public string PCreateDateTime => CreateDateTime.ToPersianDateTime();


        public string Description { get; set; }


        public IEnumerable<LessonViewModel> Lessons { get; set; }
    }
}
