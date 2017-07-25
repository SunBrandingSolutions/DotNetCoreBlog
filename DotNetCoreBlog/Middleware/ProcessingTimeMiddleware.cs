using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DotNetCoreBlog.Middleware
{
    public class ProcessingTimeMiddleware
    {
        private readonly RequestDelegate _next;

        public ProcessingTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            const string key = "X-Processing-Time-Milliseconds";

            var watch = new Stopwatch();
            watch.Start();

            await _next(context);
            
            context.Response.Headers.Add(key, new[] { watch.ElapsedMilliseconds.ToString() });
        }
    }
}
