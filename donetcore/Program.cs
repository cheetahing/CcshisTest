using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Consul;
using Exceptionless.NLog;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Web;
using Quartz;
using Quartz.Impl;
using Topshelf;
using Winton.Extensions.Configuration.Consul;

namespace donetcore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new LoggingConfiguration();
            //var configTarget = new ExceptionlessTarget()
            //{
            //    ApiKey = "ovG91wLK5RWZ8CUSMbYZgUQbhAQZfpIntQ9v8BKz",
            //    Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception}",
            //    ServerUrl = "http://localhost:50000",
            //    Name = "Sheepdata",
            //    OptimizeBufferReuse = false,
            //};

            //var consoleTarget = new ColoredConsoleTarget("target1")
            //{
            //    Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception}"
            //};



            ////config.AddRuleForAllLevels(configTarget);
            ////config.AddTarget(configTarget);

            //config.AddRuleForAllLevels(consoleTarget);
            //config.AddTarget(consoleTarget);


            //LogManager.Configuration = config;

            //LogManager.GetLogger("t").Debug("123123");


            CreateWebHostBuilder(args).ConfigureAppConfiguration(cac =>
            {
                ConsulClientConfiguration consulClientConfiguration = new ConsulClientConfiguration()
                {
                    Address = new Uri($"http://127.0.0.1:8500")
                };
                var cancellationTokenSource = new CancellationTokenSource();
                var builder = new ConfigurationBuilder()
                    .AddConsul("root", cancellationTokenSource.Token, p =>
                    {
                        p.ConsulConfigurationOptions = c => c = consulClientConfiguration;
                        p.Optional = true;
                    });
                var consulConfig = builder.Build();

                cac.AddConfiguration(consulConfig);

            })
            .Build().Run();




        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
