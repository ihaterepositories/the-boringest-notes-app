using TheMostBoringNotesApp.Services;
using TheMostBoringNotesApp.Services.Enums;
using TheMostBoringNotesApp.View.Notifiers.Interfaces;
using TheMostBoringNotesApp.View.ViewCores.Console.Snippets;

namespace TheMostBoringNotesApp.View.ViewCores.Console.CommandsFactories;

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
            _outputSnippetsHolder.ShowTasks(_taskService.GetAll(TaskSortType.Content, -1));
            return;
        }
        
        switch (args[0])
        {
            case "date": ReadByDate(args[1]); break;
            case "sort": ReadSorted(args[1]); break;
            case "all": _taskService.GetAll(TaskSortType.Content, -1); break;
            default:
                _outputSnippetsHolder.ShowUnknownCommandMessage();
                break;
        }
    }
    
    private void ReadByDate(string arg)
    {
        switch (arg)
        {
            case "today":
                _outputSnippetsHolder.ShowTasks(_taskService.GetByDate(TaskGetByDateOption.Today));
                break;
            case "yesterday":
                _outputSnippetsHolder.ShowTasks(_taskService.GetByDate(TaskGetByDateOption.Yesterday));
                break;
            case "thisWeek":
                _outputSnippetsHolder.ShowTasks(_taskService.GetByDate(TaskGetByDateOption.ThisWeek));
                break;
            case "lastWeek":
                _outputSnippetsHolder.ShowTasks(_taskService.GetByDate(TaskGetByDateOption.LastWeek));
                break;
            case "thisMonth":
                _outputSnippetsHolder.ShowTasks(_taskService.GetByDate(TaskGetByDateOption.ThisMonth));
                break;
            case "lastMonth":
                _outputSnippetsHolder.ShowTasks(_taskService.GetByDate(TaskGetByDateOption.LastMonth));
                break;
            case "thisYear":
                _outputSnippetsHolder.ShowTasks(_taskService.GetByDate(TaskGetByDateOption.ThisYear));
                break;
            case "lastYear":
                _outputSnippetsHolder.ShowTasks(_taskService.GetByDate(TaskGetByDateOption.LastYear));
                break;
            default: _notificator.NotifyWarning("Unknown date filter"); break;
        }
    }
    
    private void ReadSorted(string arg)
    {
        switch (arg)
        {
            case "ascContent": _outputSnippetsHolder.ShowTasks(_taskService.GetAll(TaskSortType.Content, 1)); break;
            case "descContent": _outputSnippetsHolder.ShowTasks(_taskService.GetAll(TaskSortType.Content, -1)); break;
            case "ascCreated": _outputSnippetsHolder.ShowTasks(_taskService.GetAll(TaskSortType.CreatedAt, 1)); break;
            case "descCreated": _outputSnippetsHolder.ShowTasks(_taskService.GetAll(TaskSortType.CreatedAt, -1)); break;
            case "ascUpdated": _outputSnippetsHolder.ShowTasks(_taskService.GetAll(TaskSortType.UpdatedAt, 1)); break;
            case "descUpdated": _outputSnippetsHolder.ShowTasks(_taskService.GetAll(TaskSortType.UpdatedAt, -1)); break;
            default: _notificator.NotifyWarning("Unknown sort type"); break;
        }
    }
}