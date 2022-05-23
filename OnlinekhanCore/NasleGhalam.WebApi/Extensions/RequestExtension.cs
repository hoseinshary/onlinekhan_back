using System;
using System.Net.Http;
using System.Web;
using NasleGhalam.Common;

namespace NasleGhalam.WebApi.Extensions
{
    public static class RequestExtension
    {
        public static int GetUserId(this HttpRequestMessage request)
        {
            var obj = request.Properties["_user_id"];
            return Convert.ToInt32(obj);
        }

        public static bool GetIsAdmin(this HttpRequestMessage request)
        {
            var obj = request.Properties["_isAdmin"];
            return Convert.ToBoolean(obj);
        }

        public static string GetAccess(this HttpRequestMessage request)
        {
            var obj = request.Properties["_access"];
            return Convert.ToString(obj);
        }

        public static byte GetRoleLevel(this HttpRequestMessage request)
        {
            var obj = request.Properties["_roleLevel"];
            return Convert.ToByte(obj);
        }

        public static UserType GetUserType(this HttpRequestMessage request)
        {
            var obj = request.Properties["_userType"];
            var userType = Convert.ToInt32(obj);
            return (UserType)Enum.ToObject(typeof(UserType), userType);
        }


        public static BrowserInfoViewModel GetBrowserInfo(this HttpRequestBase request)
        {
            var browser = request.Browser;
            return new BrowserInfoViewModel
            {
                Type = browser.Type,
                Name = browser.Browser,
                Version = browser.Version,
                MajorVersion = browser.MajorVersion,
                MinorVersion = browser.MinorVersion,
                Platform = browser.Platform,
                IsBeta = browser.Beta,
                IsCrawler = browser.Crawler,
                IsAol = browser.AOL,
                IsWin16 = browser.Win16,
                IsWin32 = browser.Win32,
                SupportsFrames = browser.Frames,
                SupportsTables = browser.Tables,
                SupportsCookies = browser.Cookies,
                SupportsVbScript = browser.VBScript,
                SupportsJavaScript = browser.EcmaScriptVersion.ToString(),
                SupportsJavaApplets = browser.JavaApplets,
                SupportsActiveXControls = browser.ActiveXControls,
                SupportsJavaScriptVersion = browser["JavaScriptVersion"],
                Ip = request.GetIpAddress(),
                UserAgent = browser.Browser,
                Device = browser.IsMobileDevice ? "Mobile" : "Desktop"
            };
        }

        public static string GetIpAddress(this HttpRequestBase request)
        {
            string ip;
            try
            {
                ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(ip))
                {
                    if (ip.IndexOf(",", StringComparison.Ordinal) > 0)
                    {
                        string[] ipRange = ip.Split(',');
                        int le = ipRange.Length - 1;
                        ip = ipRange[le];
                    }
                }
                else
                {
                    ip = request.UserHostAddress;
                }
            }
            catch { ip = null; }

            return ip;
        }
    }
}