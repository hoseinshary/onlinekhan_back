using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onlinekhan.SSO.DomainClasses.Entities
{
    public class Login
    {
        public int Id { get; set; }

        public User User { get; set; }

        public int User_id { get; set; }

        public DateTime DateTime { get; set; }

        public  string Ip { get; set; }


    }
}
