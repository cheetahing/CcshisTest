﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Exceptionless;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Config;
using Exceptionless.NLog;
using NLog;
using NLog.Web.AspNetCore;
using NLog.Extensions.Logging;
using NLog.Web;
using NLog.Web.AspNetCore.LayoutRenderers;

namespace donetcore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            //使用autofac管理DI
            //services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            RegisterCap(services);
            //services.   .AddLogging(p => { p.(); p.AddNLog(); });
            //services.AddNLog();
            //loggerFactory.AddNLog();


            services.AddLogging(p => p.AddNLog());

            //使用了autofac管理整个注入
            var serviceProvider = RegisterAutofac(services);

            return serviceProvider;
        }

        private void RegisterCap(IServiceCollection services)
        {
            services.AddCap(x =>
            {
                //如果你使用的ADO.NET，根据数据库选择进行配置：
                x.UseSqlServer(ConnnectionFactory.ConnectionString);
                //x.UseMySql("数据库连接字符串");
                //x.UsePostgreSql("数据库连接字符串");

                ////如果你使用的 MongoDB，你可以添加如下配置：
                //x.UseMongoDB("ConnectionStrings");  //注意，仅支持MongoDB 4.0+集群

                //CAP支持 RabbitMQ、Kafka、AzureServiceBus 等作为MQ，根据使用选择配置：
                x.UseRabbitMQ(p =>
                {
                    p.HostName = "localhost";
                    p.Port = 5672;
                    p.VirtualHost = "test";
                    p.UserName = "test";
                    p.Password = "test";
                });
                //x.UseRabbitMQ("localhost");
                //x.UseKafka("ConnectionStrings");
                //x.UseAzureServiceBus("ConnectionStrings");
            });
        }

        /// <summary>
        /// 使用autofac管理注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceProvider RegisterAutofac(IServiceCollection services)
        {

            //实例化Autofac容器
            var builder = new ContainerBuilder();
            //新模块组件注册    
            builder.RegisterModule<AutofacModuleRegister>();
            //将Services中的服务填充到Autofac中
            builder.Populate(services);
            //创建容器
            var Container = builder.Build();


            //第三方IOC接管 core内置DI容器 
            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            //ExceptionlessClient.Default.Configuration.ApiKey = Configuration.GetSection("Exceptionless:ApiKey").Value;
            //ExceptionlessClient.Default.Configuration.ServerUrl = Configuration.GetSection("Exceptionless:ServerUrl").Value;


            var config = new LoggingConfiguration();
            var configTarget = new ExceptionlessTarget()
            {
                ApiKey = "ovG91wLK5RWZ8CUSMbYZgUQbhAQZfpIntQ9v8BKz",
                Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception}",
                ServerUrl = "http://localhost:50000",
                Name = "Sheepdata",
                OptimizeBufferReuse = false,
            };
            config.AddTarget(configTarget);
            config.AddRuleForAllLevels(configTarget);

            NLog.Web.NLogBuilder.ConfigureNLog(config);

            //var consoleTarget = new ColoredConsoleTarget("target1")
            //{
            //    Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception}"
            //};

            loggerFactory.AddNLog();


            Exceptionless.ExceptionlessClient.Default.Configuration.ApiKey = "ovG91wLK5RWZ8CUSMbYZgUQbhAQZfpIntQ9v8BKz";
            Exceptionless.ExceptionlessClient.Default.Configuration.ServerUrl = "http://localhost:50000";

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
