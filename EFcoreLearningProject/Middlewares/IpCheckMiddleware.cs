using Microsoft.Extensions.Caching.Memory;

namespace EFcoreLearningProject.Middlewares
{
    public class IpCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions _memoryCacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
        };
        public IpCheckMiddleware(RequestDelegate next, IMemoryCache memoryCache)
        {
            _next = next;
            _memoryCache = memoryCache;
        }
        public async Task InvokeAsync(HttpContext context) 
        {
            if (context.Connection.RemoteIpAddress == null) throw new Exception("you dont have ip address");
            var ip = context.Connection.RemoteIpAddress.ToString();
            List<DateTime> value = _memoryCache.GetOrCreate<List<DateTime>>(ip, entry => {
                entry.SetOptions(_memoryCacheOptions);
                return new List<DateTime> { DateTime.Now };
            }) ?? throw new Exception("Error");
            value = value.Where(v => (DateTime.Now - v).TotalMinutes < 1).ToList();
            if (value.Count > 10)
            {    
                context.Response.StatusCode = 429;
                await context.Response.WriteAsync($"Вы сделали 10/10 запросов. Подождите {60 - Math.Ceiling((DateTime.Now - value.Min(v => v)).TotalSeconds)} секунд");
                return;
            }
            value.Add(DateTime.Now);
            _memoryCache.Set(ip, value, _memoryCacheOptions);
            Console.WriteLine($"IP: {ip} | Запрос № {value.Count - 1}| Время: {DateTime.UtcNow}");
            
            await _next(context);
        }
    }
}
