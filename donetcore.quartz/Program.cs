using Autofac;
using Autofac.Extras.Quartz;
using donetcore.quartz.Quartz;
using Quartz;
using Quartz.Impl;
using Quartz.Server;
using System;
using Topshelf;

namespace donetcore.quartz
{
    class Program
    {

        static void Main(string[] args)
        {
            ////实例化Autofac容器
            //var builder = new ContainerBuilder();
            ////新模块组件注册    
            //builder.RegisterModule<AutofacModuleRegister>();

            ////创建容器
            //var container = builder.Build();

            //HostFactory.Run(x =>
            //{
            //x.RunAsLocalSystem();

            //x.SetDescription("我的测试服务描述");
            //x.SetDisplayName("displayName");
            //x.SetServiceName("servericeName");

            //x.Service(factory =>
            //{

            //IScheduler scheduler = container.Resolve<IScheduler>();
            StdSchedulerFactory autofacSchedulerFactory = new StdSchedulerFactory(Configuration.configuration);
            var scheduler = autofacSchedulerFactory.GetScheduler().Result;
            IJobDetail job = JobBuilder.Create<TestJob>()
                                  .WithIdentity("testJob1", "test")
                                  .WithDescription("testJobDesc")
                                  .Build();
            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                                         .StartNow()
                                         .EndAt(DateTime.Now.AddDays(1))
                                         .WithIdentity("testjob1", "test1")
                                         .WithCronSchedule("* * * * * ? *")
                                         .WithDescription("trige")
                                         .Build();
            scheduler.ScheduleJob(job, trigger);
            //scheduler.DeleteJob(new JobKey("testJob1", "test"));
            scheduler.Start();

            //return server as QuartzServer;
            //});
            //});
            Console.Read();
        }
    }
}
