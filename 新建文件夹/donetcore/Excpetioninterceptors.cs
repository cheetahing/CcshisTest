using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace donetcore
{
    public class Excpetioninterceptors : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch(Exception e)
            {
                //todo:do something
            }
        }
    }
}
