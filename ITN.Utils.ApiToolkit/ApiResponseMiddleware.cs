using ITN.Utils.ApiToolkit.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace ITN.Utils.ApiToolkit
{
    public class ApiResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly string[] _ignorePaths = { "/swagger", "/health", "/favicon.ico" };

        public ApiResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Bỏ qua các path không cần wrap
            if (_ignorePaths.Any(p => context.Request.Path.StartsWithSegments(p, StringComparison.OrdinalIgnoreCase)))
            {
                await _next(context);
                return;
            }

            // Lấy hoặc tạo TraceId
            var traceId = context.Request.Headers.TryGetValue("X-Trace-Id", out var headerTraceId)
                ? headerTraceId.ToString()
                : Guid.NewGuid().ToString();
            context.Items["TraceId"] = traceId;

            try
            {
                var originalBodyStream = context.Response.Body;
                using var memoryStream = new MemoryStream();
                context.Response.Body = memoryStream;

                await _next(context);

                memoryStream.Seek(0, SeekOrigin.Begin);
                var bodyText = await new StreamReader(memoryStream).ReadToEndAsync();
                memoryStream.Seek(0, SeekOrigin.Begin);

                object? bodyData = null;
                if (!string.IsNullOrWhiteSpace(bodyText) && context.Response.ContentType?.Contains("application/json") == true)
                {
                    try { bodyData = JsonSerializer.Deserialize<object>(bodyText); }
                    catch { bodyData = bodyText; }
                }

                var success = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300;
                var response = new ApiResponse
                {
                    Success = success,
                    Data = success ? bodyData : null,
                    Error = success ? null : new ApiError
                    {
                        Code = $"HTTP_{context.Response.StatusCode}",
                        Message = bodyData?.ToString() ?? "An error occurred"
                    },
                    Timestamp = DateTime.UtcNow,
                    TraceId = traceId
                };

                context.Response.ContentType = "application/json";
                var json = JsonSerializer.Serialize(response);
                context.Response.ContentLength = System.Text.Encoding.UTF8.GetByteCount(json);

                context.Response.Body = originalBodyStream;
                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, traceId);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, string traceId)
        {
            var response = new ApiResponse
            {
                Success = false,
                Data = null,
                Error = new ApiError
                {
                    Code = "INTERNAL_SERVER_ERROR",
                    Message = ex.Message
                },
                Timestamp = DateTime.UtcNow,
                TraceId = traceId
            };

            var json = JsonSerializer.Serialize(response);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(json);
        }

    }
}
