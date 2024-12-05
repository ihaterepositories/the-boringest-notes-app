using TheMostBoringNotesApp.View.Notifiers.Interfaces;
using Task = TheMostBoringNotesApp.Models.Task;

namespace TheMostBoringNotesApp.Utils.Validators;

public class TaskValidator
{
    public string ErrorMessage { get; private set; } = string.Empty;

    public bool Validate(Task task)
    {
        if (string.IsNullOrWhiteSpace(task.Content))
        {
            ErrorMessage = "Task content cannot be empty";
            return false;
        }
        
        if (task.Content.Length > 50)
        {
            ErrorMessage = "Task content is too long";
            return false;
        }
        
        return true;
    }
}