using TodoAPI.Models;

namespace TodoAPI.Services.Validating;

public class TodoValidator
{
    public string ErrorMessage { get; private set; } = string.Empty;

    public bool Validate(Todo todo)
    {
        if (string.IsNullOrWhiteSpace(todo.Content))
        {
            ErrorMessage = "Todo content cannot be empty";
            return false;
        }
        
        if (todo.Content.Length > 50)
        {
            ErrorMessage = "Todo content is too long";
            return false;
        }
        
        return true;
    }
}