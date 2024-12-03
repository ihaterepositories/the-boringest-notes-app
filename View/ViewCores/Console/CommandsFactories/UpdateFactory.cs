using TheMostBoringNotesApp.Services;
using TheMostBoringNotesApp.View.Notifiers.Interfaces;

namespace TheMostBoringNotesApp.View.ViewCores.Console.CommandsFactories;

public class UpdateFactory
{
    private readonly TaskService _taskService;
    private readonly INotificator _notificator;
    
    public UpdateFactory(
        TaskService taskService, 
        INotificator notificator)
    {
        _taskService = taskService;
        _notificator = notificator;
    }
    
    public void Update(string[] args)
    {
        if (args.Length < 2)
        {
            _notificator.NotifyWarning("Provide task id and content");
            return;
        }
        
        if (int.TryParse(args[0], out int id))
        {
            _taskService.UpdateContent(id, args[1]);
        }
        else
        {
            _notificator.NotifyWarning("Provide valid task id");
        }
    }
    
    public void Mark(string[] args)
    {
        if (args.Length == 0)
        {
            _notificator.NotifyWarning("Provide task id");
            return;
        }
        
        if (int.TryParse(args[0], out int id))
        {
            var response = _taskService.GetById(id);
            
            if (!response.IsSuccess)
            {
                _notificator.NotifyWarning(response.Message);
                return;
            }
            
            var task = response.Data!;
            task.ChangeStatus();
            _notificator.Notify("Task marked as "+ (task.IsDone ? "done" : "undone"));
        }
        else
        {
            _notificator.NotifyWarning("Provide valid task id");
        }
    }
}