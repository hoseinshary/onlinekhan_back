using System.Collections.Generic;


namespace NasleGhalam.DomainClasses.Entities
{
    public class Province
    {
        public Province()
        {
            Cities = new HashSet<City>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
