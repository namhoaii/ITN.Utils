
# ApiResponseWrapper Middleware

A middleware for ASP.NET Core Web API to standardize API responses into a consistent format.

## Features
- Automatically wraps all responses into a consistent structure.
- Supports success and error responses.
- Allows full customization of the response pattern.
- Includes timestamp and trace ID for better debugging.

---

## Installation

```bash
dotnet add package ITN.Utils.ApiToolkit 
```

_or_

```PM
Install-Package ITN.Utils.ApiToolkit 
```

---

## Basic Usage

### 1. Add the middleware
```csharp
app.UseApiResponseWrapper();
```

### 2. In your controllers, just return standard results:
```csharp
[HttpGet("{id}")]
public IActionResult GetUser(int id)
{
    var user = _userService.GetById(id);
    if (user == null)
        return NotFound(new { code = "USER_NOT_FOUND", message = "User does not exist" });

    return Ok(user);
}
```
The middleware will convert responses into:
```json
{
  "success": true,
  "data": { ... },
  "error": null,
  "timestamp": "2025-08-10T10:00:00Z",
  "traceId": "af32bd77-1abf-4a3d-bf4f-91a61e7d7e7b"
}
```

---

## Custom Usage

You can customize the response pattern by passing options:

```csharp
app.UseApiResponseWrapper(options =>
{
    options.ResponsePattern = (context, success, data, error) => new
    {
        status = success ? "OK" : "FAIL",
        result = data,
        errorDetail = error,
        time = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
        traceId = context.TraceIdentifier
    };
});
```

Example error response:
```json
{
  "status": "FAIL",
  "result": null,
  "errorDetail": {
    "code": "USER_NOT_FOUND",
    "message": "User does not exist"
  },
  "time": "2025-08-10 10:00:00",
  "traceId": "af32bd77-1abf-4a3d-bf4f-91a61e7d7e7b"
}
```

---

## License
MIT
