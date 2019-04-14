using Microsoft.AspNetCore.Http;
using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace donetcore
{
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                throw;
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception == null) return;

            context.Response.StatusCode = 200;
            context.Response.ContentType = "text/json";
            var result = new
            {
                state = 99,
                message = exception.Message,
                StackTrace=exception.StackTrace
            };

            await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(result));
        }

    }
}
