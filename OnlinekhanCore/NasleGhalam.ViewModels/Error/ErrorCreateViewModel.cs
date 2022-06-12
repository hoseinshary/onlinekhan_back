using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ViewModels.Teacher
{
    public class ErrorCreateViewModel
    {
        public int ErrorCode { get; set; }
        public string Route { get; set; }
        public int UserId { get; set; }
        public string Ip { get; set; }
        public string Description { get; set; } 
    }
}
