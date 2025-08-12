using Microsoft.AspNetCore.Http;

namespace ITN.Utils.ApiToolkit.Models
{
    public class CustomResponseOptions
    {
        public Func<HttpContext, bool, object?, object?, object> ResponsePattern { get; set; } =
        (context, success, data, error) => new
        {
            success,
            data,
            error,
            timestamp = DateTime.UtcNow,
            traceId = context.TraceIdentifier
        };
    }
}
