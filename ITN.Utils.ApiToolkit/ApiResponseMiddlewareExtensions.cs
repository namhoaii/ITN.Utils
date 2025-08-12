using Microsoft.AspNetCore.Builder;

namespace ITN.Utils.ApiToolkit
{
    public static class ApiResponseMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiResponseWrapper(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiResponseMiddleware>();
        }
    }
}
