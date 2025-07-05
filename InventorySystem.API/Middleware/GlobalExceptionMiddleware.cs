using FluentValidation;

namespace InventorySystem.API.NewFolder;
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;

    public GlobalExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
    {
        _next = next;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            Log.Warning(ex, "Validation error caught by global middleware.");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 400;

            var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();

            var response = Response<string>.FailureResponse(
                message: "Validation failed",
                errors: errors,
                statusCode: 400
            );

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Unhandled exception caught by global middleware.");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            var errors = _env.IsDevelopment()
                ? new List<string> { ex.Message }
                : new List<string> { "An unexpected error occurred" };

            var response = Response<string>.FailureResponse(
                message: "An unexpected error occurred. Please try again later.",
                errors: errors,
                statusCode: 500
            );

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
    }
}