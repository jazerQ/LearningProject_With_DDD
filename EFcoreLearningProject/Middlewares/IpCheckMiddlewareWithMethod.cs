using Microsoft.Extensions.Caching.Memory;

namespace EFcoreLearningProject.Middlewares
{
    public class IpCheckMiddlewareWithMethod
    {
        //Условие: Ограничить POST-запросы с одного IP — не более 5 в минуту, но GET-запросы не ограничивать.
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions _options = new MemoryCacheEntryOptions() {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
        };
        public IpCheckMiddlewareWithMethod(RequestDelegate next, IMemoryCache memoryCache)
        {
            _next = next;
            _memoryCache = memoryCache;
        }
        public async Task InvokeAsync(HttpContext context) 
        {
            if (context.Connection.RemoteIpAddress == null) throw new Exception("Not Found IpAddress");
            var ip = context.Connection.RemoteIpAddress.ToString();
            if(context.Request.Method == HttpMethod.Post.ToString())
            {
                List<DateTime> value = _memoryCache.GetOrCreate<List<DateTime>>(ip, entry => {
                    entry.SetOptions(_options);
                    return new List<DateTime>() { DateTime.Now };
                }) ?? throw new Exception("Error While Creating new record in MemoryCache");

                value = value.Where(v => (DateTime.Now - v).TotalMinutes < 1).ToList();

                if (value.Count > 5) 
                {
                    context.Response.StatusCode = 429;
                    await context.Response.WriteAsync($"Too much post requests. Wait {60 - Math.Ceiling((DateTime.Now - value.First()).TotalSeconds)} seconds");
                    return;
                }
                value.Add(DateTime.Now);
                _memoryCache.Set(ip, value, _options);
                Console.WriteLine($"IP: {ip} | Запрос № {value.Count - 1}| Время: {DateTime.UtcNow}");
            }
            await _next.Invoke(context);
        }
    }
}
