using Serilog;
using Serilog.Core;

namespace NasleGhalam.Common
{
    public class Logs
    {
        public static Logger SeqLogger;
        //public static Logger TestLogger;

        public static void Register()
        {
            SeqLogger = new LoggerConfiguration()
                .MinimumLevel
                .Verbose()
                .WriteTo.Seq("http://localhost:5341/")
                .CreateLogger();

            //TestLogger = new LoggerConfiguration()
            //    .MinimumLevel
            //    .Verbose()
            //    .WriteTo.File(HttpContext.Current.Server.MapPath("~/App_Data/Serilogs/Test/Log.txt"),
            //        rollingInterval: RollingInterval.Day)
            //    .CreateLogger();
        }

        //public static void PerformanceLog(string functionName, long elapsedMilliseconds, long ignoreLogLessThan = 0)
        //{
        //    if (ignoreLogLessThan > elapsedMilliseconds)
        //        return;

        //    var executionSeconds = elapsedMilliseconds / 1000;
        //    var executionMilliseconds = elapsedMilliseconds % 1000;

        //    SeqLogger.Information("Time elapsed for ({functionName}) is ({executionTime}.{executionMilliseconds}) seconds.",
        //        functionName, executionSeconds, executionMilliseconds);
        //}
    }
}
