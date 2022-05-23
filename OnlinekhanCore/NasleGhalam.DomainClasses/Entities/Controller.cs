using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Controller
    {
        public Controller()
        {
            Actions = new HashSet<Action>();
        }
        public int Id { get; set; }

        public string FaName { get; set; }

        public string EnName { get; set; }

        public string Icone { get; set; }

        public byte Priority { get; set; }

        public int ModuleId { get; set; }

        public Module Module { get; set; }

        public ICollection<Action> Actions { get; set; }
    }
}
