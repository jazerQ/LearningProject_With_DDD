using System.Text;
using EFcoreLearningProject.Middlewares.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Npgsql.Internal;

namespace EFcoreLearningProject.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string path = "C:\\Users\\jazer\\source\\repos\\EFcoreLearningProject\\EFcoreLearningProject\\LogInfo\\logg.txt";
        public LoggerMiddleware(RequestDelegate next) 
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context) 
        {
            LoggerModel loggerModel = await LoggerModel.Create(context.Request);
            using (var writer = new StreamWriter(path, true))
            {
                await writer.WriteLineAsync($"Method: {loggerModel.Method}");
                await writer.WriteLineAsync($"Display Url: {loggerModel.DisplayUrl}");
                await writer.WriteLineAsync($"Encoded Url: {loggerModel.EncodedUrl}");
                await writer.WriteLineAsync("Header Consist Of:");
                foreach (var pair in loggerModel.Header) {
                    await writer.WriteLineAsync($"{pair.Key} - {pair.Value}");
                }
                if (loggerModel.Body != null) 
                {
                    await writer.WriteLineAsync("Body Consist Of:");
                    foreach (var pair in loggerModel.Body) 
                    {
                        await writer.WriteLineAsync($"{pair.Key} - {pair.Value}");
                    }
                }
                await writer.WriteLineAsync("\n\n\n");
            }
            Console.WriteLine($"Method: {loggerModel.Method}");
            Console.WriteLine($"Display Url: {loggerModel.DisplayUrl}");
            Console.WriteLine($"Encoded Url: {loggerModel.EncodedUrl}");
            Console.WriteLine("Header Consist Of:");
            foreach (var pair in loggerModel.Header)
            {
                Console.WriteLine($"{pair.Key} - {pair.Value}");
            }
            if (loggerModel.Body != null)
            {
                Console.WriteLine("Body Consist Of:");
                foreach (var pair in loggerModel.Body)
                {
                    Console.WriteLine($"{pair.Key} - {pair.Value}");
                }
            }
            await _next.Invoke(context);
        }
    }
}
