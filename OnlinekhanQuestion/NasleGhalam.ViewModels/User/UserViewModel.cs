using NasleGhalam.Common;

namespace NasleGhalam.ViewModels.User
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }

        public string FullName => $"{Name} {Family}";

        public string Username { get; set; }

        public bool IsActive { get; set; }

        public string NationalNo { get; set; }

        public bool Gender { get; set; }

        public string GenderName => Gender ? "پسر" : "دختر";

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }

        public int ProvinceId { get; set; }

        public string ProfilePic { get; set; }

        public string UserPicturePath => $"/Api/User/GetPictureFile/{ProfilePic}".ToFullRelativePath();

        
    }
}

