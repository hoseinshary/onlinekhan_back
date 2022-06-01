using System.Collections.Generic;

namespace Onlinekhan.SSO.DomainClasses.Entities
{
    public class City
    {
        public City()
        {
            Users = new HashSet<User>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProvinceId { get; set; }

        public  Province Province { get; set; }

        public ICollection<User> Users { get; set; }



    }
}
