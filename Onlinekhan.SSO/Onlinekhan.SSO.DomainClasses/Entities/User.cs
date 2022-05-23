using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onlinekhan.SSO.DomainClasses.Entities
{
    public class User
    {
        public User()
        {

        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string NationalNo { get; set; }

        public bool Gender { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }

        public DateTime LastLogin { get; set; }

 

        //public City City { get; set; }

        //public int CityId { get; set; }

   

        public string ProfilePic { get; set; }



        public Site Site { get; set; }

        public int SiteId { get; set; }


        public ICollection<Login> Logins { get; set; }



    }
}
