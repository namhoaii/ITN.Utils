using ITN.Utils.ApiToolkit.Models;
using Microsoft.AspNetCore.Builder;

namespace ITN.Utils.ApiToolkit
{
    public static class ApiResponseMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiResponseWrapper(this IApplicationBuilder builder, Action<CustomResponseOptions>? configureOptions = null)
        {
            var options = new CustomResponseOptions();

            configureOptions?.Invoke(options);

            return builder.UseMiddleware<ApiResponseMiddleware>(options);
        }
    }
}
