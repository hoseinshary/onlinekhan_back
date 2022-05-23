namespace NasleGhalam.DomainClasses.Entities
{
    public class Action
    {
        public int Id { get; set; }

        public string FaName { get; set; }

        public short ActionBit { get; set; }

        public byte Priority { get; set; }

        public bool IsIndex { get; set; }

        public int ControllerId { get; set; }

        public Controller Controller { get; set; }
    }
}
