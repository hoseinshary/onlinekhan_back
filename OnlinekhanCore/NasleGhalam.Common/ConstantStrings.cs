using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasleGhalam.Common
{
    public static class ConstantSettings
    {
        public const string IsCriticalIssue = nameof(IsCriticalIssue);
        public const string CriticalIssueMessage = nameof(CriticalIssueMessage);
        public const string IsUnderConstruction = nameof(IsUnderConstruction);
        public const string UnderConstructionMessage = nameof(UnderConstructionMessage);
        public const string PreferredCaptchaWebService = nameof(PreferredCaptchaWebService);

        public const string ApiVersion = "api/v1";
        public const string NotFoundHttp = "HTTP/1.1 404 Not Found";
        public const string NotFound = "یافت نشد.";
        public const string Error = "خطایی رخ داده است";
        public const string Unauthorized = "عدم دسترسی";

        public const int MaxCountOfTry = 3;

        public static readonly string WinBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public static readonly string ApiUrl = "http://159.69.82.251/api/";
        //public static readonly string ApiUrl = "http://localhost:63839/api/";//local
    }
}
