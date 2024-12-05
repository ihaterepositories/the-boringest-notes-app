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
                ShowTasksFromService(TaskSortType.CreatedAt, -1);
                break;
            case "today":
                ShowTasksFromService(TaskGetByDateOption.Today);
                break;
            case "yesterday":
                ShowTasksFromService(TaskGetByDateOption.Yesterday);
                break;
            case "thisWeek":
                ShowTasksFromService(TaskGetByDateOption.ThisWeek);
                break;
            case "lastWeek":
                ShowTasksFromService(TaskGetByDateOption.LastWeek);
                break;
            case "thisMonth":
                ShowTasksFromService(TaskGetByDateOption.ThisMonth);
                break;
            case "lastMonth":
                ShowTasksFromService(TaskGetByDateOption.LastMonth);
                break;
            case "thisYear":
                ShowTasksFromService(TaskGetByDateOption.ThisYear);
                break;
            case "lastYear":
                ShowTasksFromService(TaskGetByDateOption.LastYear);
                break;
            default: _notificator.NotifyWarning("Wrong argument."); break;
        }
    }
    
    private void ShowTasksFromService(TaskSortType sortOption, int order)
    {
        var response = _taskService.GetAll(sortOption, order);
        
        if (!response.IsSuccess)
        {
            _notificator.NotifyWarning(response.Message);
            return;
        }
        
        _outputSnippetsHolder.ShowTasks(response.Data!);
    }
    
    private void ShowTasksFromService(TaskGetByDateOption dateOption)
    {
        var response = _taskService.GetAll(dateOption);
        
        if (!response.IsSuccess)
        {
            _notificator.NotifyWarning(response.Message);
            return;
        }
        
        _outputSnippetsHolder.ShowTasks(response.Data!);
    }
}