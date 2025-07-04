namespace InventorySystem.Application.Models;

public class Response<T>
{
    public int StatusCode { get; set; } = 200;
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string>? Errors { get; set; }
    public T? Data { get; set; }

    public static Response<T> SuccessResponse(T data, string message = "Success", int statusCode = 200)
    {
        return new Response<T>
        {
            Success = true,
            Message = message,
            Data = data,
            StatusCode = statusCode
        };
    }

    public static Response<T> FailureResponse(
        string message = "Error",
        List<string>? errors = null,
        int statusCode = 400)
    {
        return new Response<T>
        {
            Success = false,
            Message = message,
            Errors = errors,
            StatusCode = statusCode
        };
    }
}
