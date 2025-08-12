using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITN.Utils.ApiToolkit.Models
{
    internal class ApiResponse
    {
        public bool Success { get; set; }
        public object? Data { get; set; }
        public ApiError? Error { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public required string TraceId { get; set; }
    }
}
