using TheMostBoringNotesApp.Services;
using TheMostBoringNotesApp.Services.Enums;
using TheMostBoringNotesApp.View.Notifiers.Interfaces;
using TheMostBoringNotesApp.View.ViewCores.TextInterfaces.Snippets;

namespace TheMostBoringNotesApp.View.ViewCores.TextInterfaces.CommandsFactories;

public class ReadFactory
{
    private readonly TaskService _taskService;
    private readonly OutputSnippetsHolder _outputSnippetsHolder;
    private readonly INotificator _notificator;
    
    public ReadFactory(
        TaskService taskService, 
        OutputSnippetsHolder outputSnippetsHolder, 
        INotificator notificator)
    {
        _taskService = taskService;
        _outputSnippetsHolder = outputSnippetsHolder;
        _notificator = notificator;
    }
    
    public void Read(string[] args)
    {
        if (args.Length == 0)
        {
            _notificator.NotifyWarning("Provide argument.");
        }
        
        switch (args[0])
        {
            case "all":
                _outputSnippetsHolder.ShowTasks(_taskService.GetAll(TaskSortType.CreatedAt, -1));
                break;
            case "today":
                _outputSnippetsHolder.ShowTasks(_taskService.GetAll(TaskGetByDateOption.Today));
                break;
            case "yesterday":
                _outputSnippetsHolder.ShowTasks(_taskService.GetAll(TaskGetByDateOption.Yesterday));
                break;
            case "thisWeek":
                _outputSnippetsHolder.ShowTasks(_taskService.GetAll(TaskGetByDateOption.ThisWeek));
                break;
            case "lastWeek":
                _outputSnippetsHolder.ShowTasks(_taskService.GetAll(TaskGetByDateOption.LastWeek));
                break;
            case "thisMonth":
                _outputSnippetsHolder.ShowTasks(_taskService.GetAll(TaskGetByDateOption.ThisMonth));
                break;
            case "lastMonth":
                _outputSnippetsHolder.ShowTasks(_taskService.GetAll(TaskGetByDateOption.LastMonth));
                break;
            case "thisYear":
                _outputSnippetsHolder.ShowTasks(_taskService.GetAll(TaskGetByDateOption.ThisYear));
                break;
            case "lastYear":
                _outputSnippetsHolder.ShowTasks(_taskService.GetAll(TaskGetByDateOption.LastYear));
                break;
            default: _notificator.NotifyWarning("Wrong argument."); break;
        }
    }
}