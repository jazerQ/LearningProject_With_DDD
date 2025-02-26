using System.Text;
using Microsoft.AspNetCore.Http.Extensions;
using Telegram.Bot.Requests.Abstractions;

namespace EFcoreLearningProject.Middlewares.Models
{
    public class LoggerModel
    {
        public string Method { get; private set; }
        public string DisplayUrl { get; private set; }
        public string EncodedUrl { get; private set; }
        public string Header { get; private set; }
        public string Body { get; private set; }
        private LoggerModel(string method, string encodedUrl, string displayUrl, string headers, string body)
        {
            this.Method = method;
            this.EncodedUrl = encodedUrl;
            this.DisplayUrl = displayUrl;
            this.Header = headers;
            this.Body = body;
        }
        public async static Task<LoggerModel> Create(HttpRequest request)
        {
            
            string body = string.Empty;
            string header = string.Empty;
            if (request.ContentLength.HasValue)
            {
                request.Body.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true)) 
                {
                    body = await reader.ReadToEndAsync();
                }
                request.Body.Seek(0, SeekOrigin.Begin);
            }
            foreach (var pair in request.Headers) 
            {
                header += $"{pair.Key}   -   {pair.Value}\n";
            }
            return new LoggerModel(request.Method, request.GetEncodedUrl(), request.GetDisplayUrl(), header, body);
        }
    }
}
