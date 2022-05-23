using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Module
    {
        public Module()
        {
            Controllers = new HashSet<Controller>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public byte Priority { get; set; }

        public ICollection<Controller> Controllers { get; set; }
    }
}
