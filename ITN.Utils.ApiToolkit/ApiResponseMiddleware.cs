using ITN.Utils.ApiToolkit.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text;

namespace ITN.Utils.ApiToolkit
{
    public class ApiResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly CustomResponseOptions _options;

        public ApiResponseMiddleware(RequestDelegate next, CustomResponseOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await _next(context);

            memoryStream.Seek(0, SeekOrigin.Begin);
            var bodyText = await new StreamReader(memoryStream).ReadToEndAsync();

            bool success = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300;

            object? data = null;
            object? error = null;

            try
            {
                var json = string.IsNullOrWhiteSpace(bodyText)
                    ? new Dictionary<string, object>()
                    : JsonSerializer.Deserialize<Dictionary<string, object>>(bodyText);

                if (success)
                {
                    data = json.ContainsKey("data") ? json["data"] : json;
                }
                else
                {
                    error = json.ContainsKey("error") ? json["error"] :
                            new { code = json.GetValueOrDefault("code") ?? "ERROR", message = json.GetValueOrDefault("message") ?? "Request failed" };
                }
            }
            catch
            {
                if (success)
                    data = bodyText;
                else
                    error = new { code = "ERROR", message = bodyText };
            }

            var finalResponse = _options.ResponsePattern(context, success, data, error);

            var modifiedBody = JsonSerializer.Serialize(finalResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            context.Response.ContentType = "application/json";
            context.Response.ContentLength = Encoding.UTF8.GetByteCount(modifiedBody);

            context.Response.Body = originalBodyStream;
            await context.Response.WriteAsync(modifiedBody, Encoding.UTF8);
        }
    }
}
