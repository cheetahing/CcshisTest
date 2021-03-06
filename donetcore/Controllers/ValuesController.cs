﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.CAP;
using MongoDB.Bson;
using MongoDB.Driver;
using Exceptionless;
using Microsoft.Extensions.Logging;
using NLog;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Threading;
using WebApiClient;
using Consul;
using donetcore.WebApiClient;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace donetcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICapPublisher capPublisher;
        private readonly ILogger<ValuesController> logger;
        private readonly IHealth health;
        private readonly IConfiguration configuration;
        private readonly IDistributedCache distributedCache;
        //private readonly ConnectionMultiplexer redis;


        public ValuesController(/*ICapPublisher capPublisher
            ,*/ILogger<ValuesController> logger
            , IHealth health
            ,IConfiguration configuration)
        {
            //this.capPublisher = capPublisher;
            this.logger = logger;
            this.health = health;
            this.configuration = configuration;
            //this.distributedCache = distributedCache;
            //this.redis = redis;

            //log.LogDebug("hello log");
            logger.LogDebug(logger.GetType().ToString());
        }

        [Authorize]
        [HttpPost]
        [Route("Verify")]
        public ActionResult Verify()
        {

            return Ok();
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {

            //if (request.Username == "AngelaDaddy" && request.Password == "123456")
            //{
            // push the user’s name into a claim, so we can identify the user later on.
            string userName = "zhangshan";
            string securityKey = "skjasdflskdl;fakjsdfl;aksjdfl;ajskldfjasld;kfjasl;djf";
            var claims = new[]
            {
                new Claim(ClaimTypes.Sid,"1"),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.GroupSid,"123"),
                new Claim(ClaimTypes.SerialNumber,"asdf")//这里增加全局的序列批号，用来做过期
            };
            //sign the token using a secret key.This secret will be shared between your API and anything that needs to check that the token is legit.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //.NET Core’s JwtSecurityToken class takes on the heavy lifting and actually creates the token.
            /**
                * Claims (Payload)
                Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT 标准规定了一些字段，下面节选一些字段:

                iss: The issuer of the token，token 是给谁的
                sub: The subject of the token，token 主题
                exp: Expiration Time。 token 过期时间，Unix 时间戳格式
                iat: Issued At。 token 创建时间， Unix 时间戳格式
                jti: JWT ID。针对当前 token 的唯一标识
                除了规定的字段外，可以包含其他任何 JSON 兼容的字段。
                * */
            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(300),
                signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
            //}

            //return BadRequest("Could not verify username and password");


            //var c = configuration["publish:test:student:name"];

            //throw new Exception("1231231");

            //return new string[]{ "i am in"};
            //var i = 0;
            //while (i++ < 10000)
            //{
            //using (var conn = ConnnectionFactory.Create2())
            //{
            //    using (var tran = conn.BeginTransaction(capPublisher, autoCommit: true))
            //    {
            //        this.capPublisher.PublishAsync("Create2", DateTime.Now);
            //    }
            //}

            //using (var conn = ConnnectionFactory.Create())
            //{
            //    using (var tran = conn.BeginTransaction(capPublisher, autoCommit: true))
            //    {

            //var client = HttpApiClient.Create<WebApiClient.IHealth>();
            //var gres1 = health.Get().InvokeAsync();
            //var gres2 = health.Get().InvokeAsync();

            //var a = await gres1;
            //var b = await gres2;


            //var redis = ConnectionMultiplexer.Connect("127.0.0.1");
            //while(!redis.GetDatabase().LockTake("test", "1asdf23", new TimeSpan(0, 0, 20)))
            //{
            //    Thread.Sleep(10000);
            //}
            //var re=redis.GetDatabase().StringSet(key: "123",value: "vvvffv");
            //this.redis.GetDatabase().StringGet("123");
            //redis.GetDatabase().LockRelease("test", "1asdf23");
            //try
            //{
            //    this.capPublisher.Publish("publish.valuesController", DateTime.Now, "callback_publish");
            //}
            //catch(Exception e)
            //{
            //    var inex = e;
            //}
            //    }
            //}
            //}

            //Student s = new Student()
            //{
            //    Name = "rrrr",
            //    Phone = "rrrr",
            //    Sex = 1
            //};

            //var doc=new BsonDocument()
            //{
            //    { "Name","test" },
            //    { "Phone","123" },
            //    {"23","123" },
            //    {"123","123" },
            //};

            //var db = MongodbContext.Create();
            //var collection = db.Client.GetDatabase("mydb").GetCollection<Student>("test");

            //////插入
            //collection.InsertOne(s);

            ////查找
            //var list = collection.Find<Student>(p => p.Name == "test").ToList();

            ////删除
            //s.Id = new Guid("75F78F03-6AFE-4267-A969-FF3478077498");
            //var i = collection.DeleteOne(p=>p.Id==s.Id).DeletedCount;

            //修改
            //s.Id = new Guid("FA0D74EE-9BBC-4182-B32F-0D834C224F9A");
            //var filter = Builders<Student>.Filter.Eq(p => p.Id == s.Id);
            //collection.UpdateOne(filter, s);
            //collection.ReplaceOne<Student>(p => p.Id == s.Id, s);

            //ExceptionlessClient.Default.CreateLog("123").Submit();
            //Exception e = new Exception("testExceptionLess");

            //e.ToExceptionless().Submit();

            //return new string[] { "value1", "value2" };
        }

        [NonAction]
        [CapSubscribe("publish.valuesController")]
        public void Recive(DateTime dateTime)
        {
            var datetime2 = dateTime;
        }

        [NonAction]
        [CapSubscribe("publish.valuesController")]
        public DateTime Recive2(DateTime dateTime)
        {
            var datetime2 = dateTime;
            return DateTime.Now;
        }


        [NonAction]
        [CapSubscribe("callback_publish")]
        public DateTime Recive222(DateTime dateTime)
        {
            var datetime2 = dateTime;
            return DateTime.Now;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
