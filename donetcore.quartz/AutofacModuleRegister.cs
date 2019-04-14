using Autofac;
using Autofac.Extras.Quartz;
using donetcore.quartz.Quartz;
using Quartz.Server;

namespace donetcore.quartz
{
    public class AutofacModuleRegister: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // 1) Register IScheduler
            builder.RegisterModule(new QuartzAutofacFactoryModule() { ConfigurationProvider=p=>Configuration.configuration});
            // 2) Register jobs
            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(TestJob).Assembly));

            builder.RegisterType<QuartzServer>().As<IQuartzServer>();
        }
    }
}