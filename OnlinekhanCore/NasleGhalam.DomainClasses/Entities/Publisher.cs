using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Publisher
    {
        public Publisher()
        {
            AxillaryBooks = new HashSet<AxillaryBook>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<AxillaryBook> AxillaryBooks { get; set; }
    }
}
