using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;

namespace donetcore.WebApiClient
{
    public interface IHealth: IHttpApi
    {
        [HttpGet("payment/health/")]
        ITask<string> Get();
    }
}
