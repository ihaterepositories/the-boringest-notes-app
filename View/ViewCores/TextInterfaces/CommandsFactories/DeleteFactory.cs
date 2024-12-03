using TheMostBoringNotesApp.Services;
using TheMostBoringNotesApp.View.Notifiers.Interfaces;
using TheMostBoringNotesApp.View.ViewCores.TextInterfaces.Snippets;

namespace TheMostBoringNotesApp.View.ViewCores.TextInterfaces.CommandsFactories;

public class DeleteFactory
{
    private readonly TaskService _taskService;
    private readonly OutputSnippetsHolder _outputSnippetsHolder;
    private readonly INotificator _notificator;
    
    public DeleteFactory(
        TaskService taskService, 
        OutputSnippetsHolder outputSnippetsHolder, 
        INotificator notificator)
    {
        _taskService = taskService;
        _outputSnippetsHolder = outputSnippetsHolder;
        _notificator = notificator;
    }
    
    public void Delete(string[] args)
    {
        if (args.Length == 0)
        {
            _notificator.NotifyWarning("Provide command argument");
            return;
        }
        
        if (int.TryParse(args[0], out int id))
        {
            _taskService.Delete(id);
        }
        else
        {
            switch (args[0])
            {
                case "all":
                    if (_outputSnippetsHolder.ConfirmAction("delete all tasks"))
                    {
                        _taskService.DeleteAll();
                    }
                    break;
            }
        }
    }
}