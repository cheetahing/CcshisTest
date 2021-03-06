﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;

namespace donetcore
{
    public class AutofacModuleRegister:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //注册Service中的对象,Service中的类要以Service结尾，否则注册失败
            //builder.RegisterAssemblyTypes(GetAssemblyByName("WXL.Service")).Where(a => a.Name.EndsWith("Service")).AsImplementedInterfaces();
            ////注册Repository中的对象,Repository中的类要以Repository结尾，否则注册失败
            //builder.RegisterAssemblyTypes(GetAssemblyByName("WXL.Repository")).Where(a => a.Name.EndsWith("Repository")).AsImplementedInterfaces();
            //单独注册
            //builder.RegisterType<WxPayService>().Named<IPayService>(typeof(WxPayService).Name);
            //builder.RegisterType<AliPayService>().Named<IPayService>(typeof(AliPayService).Name);

            //builder.RegisterAssemblyTypes(GetCurrentAssembly()).AsImplementedInterfaces().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);


            //builder.RegisterAssemblyTypes(GetCurrentAssembly()).Where(a=>a.Name.EndsWith("Controller")).PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<LogInterceptors>();
            builder.RegisterType<Excpetioninterceptors>();
            builder.RegisterAssemblyTypes(GetCurrentAssembly()).AsImplementedInterfaces().EnableInterfaceInterceptors().InterceptedBy(typeof(LogInterceptors),typeof(Excpetioninterceptors));
        }
        

        /// <summary>
        /// 根据程序集名称获取程序集
        /// </summary>
        /// <param name="AssemblyName">程序集名称</param>
        /// <returns></returns>
        public static Assembly GetAssemblyByName(String AssemblyName) => Assembly.Load(AssemblyName);

        public static Assembly GetCurrentAssembly() => Assembly.GetCallingAssembly();
    }
}
