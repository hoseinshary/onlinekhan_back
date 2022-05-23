using System;
using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Program
    {
        public Program()
        {
            ProgramItems = new HashSet<ProgramItem>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public bool IsMain { get; set; }

        public DateTime CreatedTime { get; set; }


        public ICollection<ProgramItem> ProgramItems { get; set; }


        public string Description { get; set; } 


    }
}
