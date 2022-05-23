using System.Collections.Generic;
using Onlinekhan.SSO.DomainClasses.Entities;


namespace Onlinekhan.SSO.DomainClasses.Entities
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