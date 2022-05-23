using System;
using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Package
    {
        public Package()
        {
            Lessons = new HashSet<Lesson>();
            Assays = new HashSet<Assay>();
        }

        public int Id { get; set; }

        public string  Name { get; set; }

        public bool IsDelete { get; set; }

        public string  ImageFile { get; set; }

        public bool IsActive { get; set; }

        public int Price { get; set; }

        public int TimeDays { get; set; }

        public DateTime CreateDateTime { get; set; }

        public string Description { get; set; }

        public ICollection<Lesson> Lessons { get; set; }

        public ICollection<Assay> Assays { get; set; }

        public ICollection<Sale_Package> Sales_Packages { get; set; }
    }
}
