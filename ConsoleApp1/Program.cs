using Exceptionless.NLog;
using NLog;
using NLog.Config;
using System;
using Exceptionless;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            Exceptionless.ExceptionlessClient.Default.Configuration.ApiKey = "ovG91wLK5RWZ8CUSMbYZgUQbhAQZfpIntQ9v8BKz";
            Exceptionless.ExceptionlessClient.Default.Configuration.ServerUrl = "http://localhost:50000";
            Exceptionless.ExceptionlessClient.Default.CreateLog($"ExceptionlessClient{DateTime.Now.ToString()}",Exceptionless.Logging.LogLevel.Error).Submit();

            //var config = new LoggingConfiguration();
            //var configTarget = new ExceptionlessTarget()
            //{
            //    ApiKey = "ovG91wLK5RWZ8CUSMbYZgUQbhAQZfpIntQ9v8BKz",
            //    Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception}",
            //    ServerUrl = "http://localhost:50000",
            //    Name = "test",
            //};
            //config.AddTarget("exceptionless", configTarget);

            //NLog.LogManager.Configuration = config;
            //NLog.LogManager.GetLogger("test").Debug($"NLog{DateTime.Now}");
            //NLog.Web.NLogBuilder.ConfigureNLog(config);

        }
    }
}
