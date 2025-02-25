using Microsoft.AspNetCore.Http.Extensions;
using Telegram.Bot.Requests.Abstractions;

namespace EFcoreLearningProject.Middlewares.Models
{
    public class LoggerModel
    {
        public string Method { get; private set; }
        public string DisplayUrl { get; private set; }
        public string EncodedUrl { get; private set; }
        public IHeaderDictionary Header { get; private set; }
        public IFormCollection? Body { get; private set; }
        private LoggerModel(string method, string encodedUrl, string displayUrl, IHeaderDictionary headers, IFormCollection? body)
        {
            this.Method = method;
            this.EncodedUrl = encodedUrl;
            this.DisplayUrl = displayUrl;
            this.Header = headers;
            if (body != null)
            {
                this.Body = body;
            }
        }
        public async static Task<LoggerModel> Create(HttpRequest request)
        {
            
            var body = await request.ReadFormAsync();
            return new LoggerModel(request.Method, request.GetEncodedUrl(), request.GetDisplayUrl(), request.Headers, body);
        }
    }
}
