using TheMostBoringNotesApp.Services;
using TheMostBoringNotesApp.View.Notifiers.Interfaces;
using TheMostBoringNotesApp.View.ViewCores.TextInterfaces.Snippets;

namespace TheMostBoringNotesApp.View.ViewCores.TextInterfaces.CommandsFactories;

public class UpdateFactory
{
    private readonly TaskService _taskService;
    private readonly INotificator _notificator;
    private readonly OutputSnippetsHolder _outputSnippetsHolder;
    
    public UpdateFactory(
        TaskService taskService,
        OutputSnippetsHolder outputSnippetsHolder,
        INotificator notificator)
    {
        _taskService = taskService;
        _outputSnippetsHolder = outputSnippetsHolder;
        _notificator = notificator;
    }
    
    public void Update(string[] args)
    {
        if (args.Length == 0)
        {
            _notificator.NotifyWarning("Provide task id");
            return;
        }
        
        if (int.TryParse(args[0], out int id))
        {
            
            var content = _outputSnippetsHolder.GetUserInput("New content:");
            _taskService.UpdateContent(id, content);
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