using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace donetcore
{

    public class LogInterceptors : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var p = invocation.Arguments;//进入切面
            invocation.Proceed();
        }
    }
}
