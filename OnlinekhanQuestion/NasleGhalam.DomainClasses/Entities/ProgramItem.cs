using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using NasleGhalam.Common;

namespace NasleGhalam.DomainClasses.Entities
{
    public class ProgramItem
    {
        public ProgramItem()
        {

        }
        public int Id { get; set; }

        public int LookupId_PrgoramItemName { get; set; }

        public Lookup Lookup_ProgramItemName { get; set; }

        public int ProgramId { get; set; }

        public Program Program { get; set; }

        public int Hour { get; set; }


        public DayOfWeak  DayOfWeak { get; set; }

        public String Description { get; set; } 


    }
}
