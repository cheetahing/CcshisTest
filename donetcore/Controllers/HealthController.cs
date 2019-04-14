using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace donetcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// 获取服务健康检查
        /// </summary>
        /// <param name="id">来源id</param>
        /// <returns></returns>
        public IActionResult Get()
        {
            return Ok("ok");
        }
    }
}