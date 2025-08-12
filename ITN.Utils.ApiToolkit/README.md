# ITN.Utils.ApiResponse

This project provides a custom middleware for ASP.NET Core to standardize API responses with a consistent format and include useful metadata such as `traceId` for request tracking.

## Features

- Automatically formats all API responses in a consistent JSON structure.
- Adds a unique `traceId` to each request for debugging and tracking purposes.
- Handles both success and error responses globally.
- Allows manual triggering of custom messages and errors from controllers.

## Standard Response Format

```json
{
  "traceId": "d1b7d25c-6f8a-4e65-a8a0-b321b42f0d5e",
  "success": true,
  "message": "Request completed successfully",
  "data": { ... }
}
```

## How It Works

1. **Middleware** intercepts all responses and wraps them in the standard format.
2. The `traceId` is generated per request and included in the response.
3. Errors and exceptions are caught and returned in the same format with `success = false`.

## Example Usage in Controller

```csharp
[HttpGet("example")]
public IActionResult Example()
{
    return ApiResponse.Ok(new { Name = "John", Age = 30 }, "Data retrieved successfully");
}

[HttpGet("error")]
public IActionResult ErrorExample()
{
    return ApiResponse.Fail("Something went wrong");
}
```

## Installation

1. Add the middleware to your ASP.NET Core project:

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseMiddleware<ApiResponseMiddleware>();
    // other middlewares...
}
```

2. Optionally, use the provided helper class `ApiResponse` for manual responses in controllers.

## License

This project is licensed under the MIT License.
