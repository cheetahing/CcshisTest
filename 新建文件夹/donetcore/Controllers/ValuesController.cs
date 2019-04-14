﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using donetcore.Service;
using Microsoft.AspNetCore.Mvc;

namespace donetcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        protected IHelloWord helloWord { private get; set; }

        public ValuesController(IHelloWord helloWord)
        {
            this.helloWord = helloWord;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var hello = helloWord.SayHelloword();
            return new string[] { "value1", "value2" };
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
