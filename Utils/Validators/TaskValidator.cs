using Task = TheMostBoringNotesApp.Models.Task;

namespace TheMostBoringNotesApp.Utils.Validators;

public class TaskValidator
{
    private readonly Logger _logger = new Logger("TaskValidator");
    
    public bool Validate(Task task)
    {
        if (string.IsNullOrWhiteSpace(task.Content))
        {
            _logger.LogError("Task validation failed, content is empty");
            return false;
        }
        
        return true;
    }
}