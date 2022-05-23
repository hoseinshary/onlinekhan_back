using System.Collections.Generic;
using NasleGhalam.Common;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public byte Level { get; set; }

        public string SumOfActionBit { get; set; }

        public UserType UserType { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
