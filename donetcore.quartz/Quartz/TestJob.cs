using Quartz;
using Quartz.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace donetcore.quartz.Quartz
{
    [Serializable]
    public class TestJob : IJob
    {
        //private readonly IQuartzServer quartzServer;
        //public TestJob(IQuartzServer quartzServer)
        //{
        //    this.quartzServer = quartzServer;
        //}

        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine(DateTime.Now.ToString());
            return Task.CompletedTask;
        }
    }
}
