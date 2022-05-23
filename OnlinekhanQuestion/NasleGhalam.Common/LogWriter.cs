using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NasleGhalam.Common
{
    public static class LogWriter
    {
        private static readonly object StaticLockObject = new object();
        public static void LogException(Exception exception, params string[] extraDescriptions)
        {
            LogException(exception.ToString(), extraDescriptions);
        }

        public static void LogException(string exception, params string[] extraDescriptions)
        {
            try
            {
                lock (StaticLockObject)
                {
                    var path = GetTodayFilePath();
                    using (var w = File.AppendText($"{path}/Errors-{DateTime.Now:HH}.txt"))
                    {
                        WriteLine($"{exception}", w, extraDescriptions);
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static void LogInfo(params string[] infos)
        {
            try
            {
                lock (StaticLockObject)
                {
                    var path = GetTodayFilePath();
                    using (var w = File.AppendText($"{path}/Info-{DateTime.Now:HH}.txt"))
                    {
                        var infoList = infos.ToList();
                        var lastMessage = infoList.LastOrDefault();
                        if (infos.Length > 0)
                        {
                            infoList.RemoveAt(infos.Length - 1);
                        }
                        WriteLine(lastMessage, w, infoList.ToArray());
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private static void WriteLine(string logMessage, TextWriter txtWriter, params string[] extraDescriptions)
        {
            try
            {

                txtWriter.WriteLine("{0:s}", DateTime.Now);
                foreach (var description in extraDescriptions)
                {
                    txtWriter.WriteLine($"{description}");
                }
                txtWriter.WriteLine(logMessage);
                txtWriter.WriteLine("-------------------------------\n\r");
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private static string GetTodayFilePath()
        {
            var path = HttpContext.Current == null ? ConstantSettings.WinBaseDirectory : HttpContext.Current.Server.MapPath("~/App_Data");
            path += $"/Logs/{DateTime.Today:MM-dd}";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }
    }
}
