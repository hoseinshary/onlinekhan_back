using System;
using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class StudentMajorList_Major
    {

        public StudentMajorList_Major()
        {

        }


        public int Id { get; set; }


        public int Priority { get; set; }


        public int StudentMajorListId { get; set; }
        public StudentMajorlist StudentMajorList { get; set; }

        
        public int MajorsId { get; set; }
        public Majors Major { get; set; }
    }
}
