using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Error
    {
        public int Id { get; set; }

        public int ErrorCode { get; set; }
        public string Route { get; set; }
        public int UserId { get; set; }
        public string Ip { get; set; }
        public string Description { get; set; }

    }
}
