using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Onlinekhan.SSO.DomainClasses.Entities
{
    public class Site
    {
        public Site()
        {
            //Users = new HashSet<User>();
        }

        public int Id { get; set; }

        
        public string Url { get; set; } 

        public string Name { get; set; }

        //public ICollection<User> Users { get; set; }

    }
}
