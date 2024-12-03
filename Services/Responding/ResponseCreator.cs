using TheMostBoringNotesApp.Services.Responding.Models;

namespace TheMostBoringNotesApp.Services.Responding;

public class ResponseCreator
{
    public ServiceResponse<T> CreateError<T>(string message)
    {
        return new ServiceResponse<T>
        {
            IsSuccess = false,
            Message = message
        };
    }
    
    public ServiceResponse<T> CreateOk<T>(T data)
    {
        return new ServiceResponse<T>
        {
            IsSuccess = true,
            Data = data,
            Message = "success"
        };
    }
}