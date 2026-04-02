using System.Collections.Generic;

namespace SarvashresthaCMS.Application.Common;

public class ServiceResponse<T>
{
    public T? Data { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool Success { get; set; } = true;
    public List<string>? Errors { get; set; }

    public static ServiceResponse<T> Ok(T data, string message = "Success")
    {
        return new ServiceResponse<T> { Data = data, Message = message, Success = true };
    }

    public static ServiceResponse<T> Fail(string message, List<string>? errors = null)
    {
        return new ServiceResponse<T> { Success = false, Message = message, Errors = errors };
    }
}
