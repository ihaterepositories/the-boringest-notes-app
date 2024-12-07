namespace TodoAPI.Services.Responding.Models;

public class ServiceResponse<T>
{
    public string Message { get; set; } = string.Empty;
    public bool IsSuccess { get; set; } = true;
    public T? Data { get; set; }
}