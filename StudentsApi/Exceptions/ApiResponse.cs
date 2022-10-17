namespace StudentsApi.Exceptions;

public class ApiResponse<T>
{
    public T? Data { get; set; }
    public bool Succeded { get; set; }
    public string? Message { get; set; }
    
    public static ApiResponse<T> Fail(string error)
    {
        return new ApiResponse<T> { Message = error, Succeded = false };
    }

    public static ApiResponse<T> Success(T data)
    {
        return new ApiResponse<T> { Succeded = true, Data = data };
    }
}
