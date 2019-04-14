using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Winton.Extensions.Configuration.Consul;

namespace donetcore
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IApplicationLifetime lifetime, ServiceEntry serviceEntity,IConfiguration configuration)
        {
            ConsulClientConfiguration consulClientConfiguration = new ConsulClientConfiguration()
            {
                Address = new Uri($"http://127.0.0.1:8500")
            };

            var consulClient = new ConsulClient(x =>x= consulClientConfiguration);//请求注册的 Consul 地址

            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔
                HTTP = $"http://localhost:51606/api/health",//健康检查地址
                Timeout = TimeSpan.FromSeconds(5)
            };

            // Register service with consul
            var registration = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                ID = "donetcore",//Guid.NewGuid().ToString(),
                Name = "my.servicename1",
                Address = "localhost",
                Port = 51606,
                Tags = new[] { $"urlprefix-/my.servicename" }//添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
            };

            consulClient.Agent.ServiceRegister(registration).Wait();//服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）
            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();//服务停止时取消注册
            });




            return app;
        }
    }
}
