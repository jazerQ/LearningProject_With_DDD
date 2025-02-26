using Microsoft.Extensions.Caching.Memory;

namespace EFcoreLearningProject.Middlewares
{
    public class IpMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions _options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
        }; 
        public IpMiddleware(RequestDelegate next, IMemoryCache memoryCache) 
        {
            _memoryCache = memoryCache;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString();
            if (ip == null) throw new Exception("Your ip is hidden");
            var elem = _memoryCache.GetOrCreate<int>(ip, entry => {
                entry.SetOptions(_options);
                return 1;

            }); 
            if (elem >= 5)
            {
                context.Response.StatusCode = 429;
                await context.Response.WriteAsync("Too many Requests");
                return;
            }
            else 
            {
                _memoryCache.Set(ip, elem + 1, _options);
            }
            
            Console.WriteLine(elem);
        

                Console.WriteLine($"\n\n\n--------------------------------\nMy Ip: {context.Connection.RemoteIpAddress?.ToString()} Time: {DateTime.UtcNow} \n---------------------------");
            await _next.Invoke(context);
        }
    }
}
