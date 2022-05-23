using NasleGhalam.Common;
using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ViewModels.Writer
{
    public class WriterViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        public string ProfilePic { get; set; }

        public string WriterPicturePath => $"/Api/Writer/GetPictureFile/{ProfilePic}".ToFullRelativePath();

        public UserViewModel User { get; set; }
    }
}
