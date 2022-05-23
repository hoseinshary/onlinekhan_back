using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ViewModels.Teacher
{
    public class TeacherViewModel
    {
        public int Id { get; set; }

        public string FatherName { get; set; }

        public string Address { get; set; }

        public UserViewModel User { get; set; }
    }
}

