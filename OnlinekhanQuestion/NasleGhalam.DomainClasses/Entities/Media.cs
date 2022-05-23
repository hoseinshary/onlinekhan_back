using System;
using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Media
    {
        public Media()
        {
         
            Topics = new HashSet<Topic>();
            

        }
        public int Id { get; set; }

        public string Title { get; set; }

        
        public int LookupId_MediaType { get; set; }
        public Lookup Lookup_MediaType { get; set; }

        
        public int WriterId { get; set; }

        public Writer Writer { get; set; }
        
        public string Description { get; set; }

        public string FileName { get; set; }

        public int Price { get; set; }

        public DateTime InsertDateTime { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public bool IsActive { get; set; }

        public string CoverImage { get; set; }

        public string  Length { get; set; }

        public int YearOfBook { get; set; }

        public string PagesOfBook { get; set; }

        
        public ICollection<Topic> Topics { get; set; }
        
        
    }
}
