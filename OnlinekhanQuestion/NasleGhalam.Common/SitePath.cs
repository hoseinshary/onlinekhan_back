using System;
using System.IO;
using System.Web;

namespace NasleGhalam.Common
{
    public static class SitePath
    {
        public static string AxillaryBookRelPath => "/Content/AxillaryBook/";
        public static string QuestionRelPath => "~/Content/Question/";
        public static string LessonRelPath => "~/Content/Lesson/";
        public static string QuestionOptionsRelPath => "~/Content/QuestionOptions/";
        public static string QuestionAnswerRelPath => "~/Content/QuestionAnswer/";
        public static string QuestionGroupRelPath => "~/Content/QuestionGroup/";
        public static string QuestionGroupTempRelPath => "~/Content/QuestionGroupTemp/";
        public static string UserProfileRelPath => "~/Content/UserProfile/";
        public static string WriterPictureRelPath => "~/Content/WriterPicture/";
        public static string PackageRelPath => "~/Content/Package/";

        public static string MediaRelPath => "~/Content/Media/";
        public static string DefaultUserProfileRelPath => "~/Content/UserProfile/DefaultProfile.png";
        public static string AssayRelPath => "/Content/Assay/";


        public static string PDFtoPNGRelPath => "~/";
        //-------------------------------------------------------------------------------------

        public static string GetQuestionAbsPath(string name) => ToAbsolutePath($"{QuestionRelPath}{name}");
        public static string GetLessonAbsPath(string name) => ToAbsolutePath($"{LessonRelPath}{name}");

        public static string GetMediaAbsPath(string name) => ToAbsolutePath($"{MediaRelPath}{name}");
        public static string GetAssayAbsPath(string name) => ToAbsolutePath($"{AssayRelPath}{name}");

        public static string GetQuestionOptionsAbsPath(string name) => ToAbsolutePath($"{QuestionOptionsRelPath}{name}");

        public static string GetQuestionAnswerAbsPath(string name) => ToAbsolutePath($"{QuestionAnswerRelPath}{name}");

        public static string GetQuestionGroupAbsPath(string name) => ToAbsolutePath($"{QuestionGroupRelPath}{name}");

        public static string GetQuestionGroupTempAbsPath(string name) => ToAbsolutePath($"{QuestionGroupTempRelPath}{name}");


        public static string GetPDFtoPNGAbsPath(string name) => ToAbsolutePath($"{PDFtoPNGRelPath}{name}");

        public static string GetUserAbsPath(string name)
        {
            var path = ToAbsolutePath($"{UserProfileRelPath}{name}");
            return File.Exists(path) ? path : ToAbsolutePath(DefaultUserProfileRelPath);
        }

        public static string GetWriterAbsPath(string name)
        {
            var path = ToAbsolutePath($"{WriterPictureRelPath}{name}");
            return File.Exists(path) ? path : ToAbsolutePath(DefaultUserProfileRelPath);
        }


        public static string ToAbsolutePath(this string relativePath)
        {
            return HttpContext.Current.Server.MapPath(relativePath);
        }

        public static string ToFullRelativePath(this string relativePath)
        {
            var baseUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            return $"{baseUrl}{relativePath}";
        }
    }
}