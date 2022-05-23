namespace NasleGhalam.ViewModels.Action
{
    public class ActionViewModel
    {
        public int Id { get; set; }

        public string ActionFaName { get; set; }

        public string ControllerFaName { get; set; }

        public bool IsChecked { get; set; }

        public short ActionBit { get; set; }

        public int ControllerId { get; set; }

        public int ModuleId { get; set; }
    }
}
