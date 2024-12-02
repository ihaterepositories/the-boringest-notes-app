using TheMostBoringNotesApp.Utils.Notifiers;
using TheMostBoringNotesApp.Utils.Notifiers.Interfaces;
using Task = TheMostBoringNotesApp.Models.Task;

namespace TheMostBoringNotesApp.Utils.Validators;

public class TaskValidator
{
    private readonly INotificator _notificator;

    public TaskValidator(INotificator notificator)
    {
        _notificator = notificator;
    }

    public bool Validate(Task task)
    {
        if (string.IsNullOrWhiteSpace(task.Content))
        {
            _notificator.NotifyError("Task validation failed, content is empty");
            return false;
        }
        
        return true;
    }
}