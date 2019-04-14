using Autofac.Extras.DynamicProxy;
using donetcore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace donetcore.ServiceImpl
{
    public class HelloWord : IHelloWord
    {
        public string SayHelloword()
        {
            return "helloWord";
        }
    }
}
