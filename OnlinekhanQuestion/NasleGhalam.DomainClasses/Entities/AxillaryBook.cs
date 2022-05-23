using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class AxillaryBook
    {
        public AxillaryBook()
        {
            EducationBooks = new HashSet<EducationBook>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public short PublishYear { get; set; }

        public string Author { get; set; }

        public string Isbn { get; set; }

        public string Font { get; set; }

        public string ImgName { get; set; }

        public int Price { get; set; }

        public int OriginalPrice { get; set; }

        public string Description { get; set; }

        public int LookupId_PrintType { get; set; }

        public int PublisherId { get; set; }

        public int LookupId_PaperType { get; set; }

        public int LookupId_BookType { get; set; }

        public Lookup Lookup_PrintType { get; set; }

        public Publisher Publisher { get; set; }

        public Lookup Lookup_BookType { get; set; }

        public Lookup Lookup_PaperType { get; set; }

        public ICollection<EducationBook> EducationBooks { get; set; }
    }
}
