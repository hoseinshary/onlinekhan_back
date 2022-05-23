using NasleGhalam.Common;
using NasleGhalam.ViewModels.Lookup;
using NasleGhalam.ViewModels.Publisher;

namespace NasleGhalam.ViewModels.AxillaryBook
{
    public class AxillaryBookViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public short PublishYear { get; set; }

        public string Author { get; set; }

        public string Isbn { get; set; }

        public string Font { get; set; }

        public int LookupId_PrintType { get; set; }

        public string ImgName { get; set; }

        public string ImgPath => string.IsNullOrEmpty(ImgName) ? "" : $"{SitePath.AxillaryBookRelPath}{ImgName}".ToFullRelativePath();

        public string ImgAbsPath => $"{SitePath.AxillaryBookRelPath.ToAbsolutePath()}{ImgName}";

        public int Price { get; set; }

        public int OriginalPrice { get; set; }

        public int LookupId_BookType { get; set; }

        public int LookupId_PaperType { get; set; }

        public string Description { get; set; }

        public int PublisherId { get; set; }

        public PublisherViewModel Publisher { get; set; }

        public LookupViewModel Lookup_BookType { get; set; }

        public LookupViewModel Lookup_PaperType { get; set; }

        public LookupViewModel Lookup_PrintType { get; set; }
    }
}
