using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ViewModels.Teacher
{
    public class ErrorViewModel
    {
        public int Id { get; set; }
        public int ErrorCode { get; set; }
        public string Route { get; set; }
        public int UserId { get; set; }
        public string Ip { get; set; }
        public string Description { get; set; }
    }
}

