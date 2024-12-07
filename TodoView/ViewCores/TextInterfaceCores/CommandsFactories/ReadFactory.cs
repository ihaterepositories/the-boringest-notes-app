using TodoAPI.Services;
using TodoAPI.Services.Enums;
using TodoView.ViewCores.TextInterfaceCores.Notifying.Interfaces;
using TodoView.ViewCores.TextInterfaceCores.Snippets;

namespace TodoView.ViewCores.TextInterfaceCores.CommandsFactories;

public class ReadFactory
{
    private readonly TodoService _todoService;
    private readonly ConsoleOutputSnippetsHolder _consoleOutputSnippetsHolder;
    private readonly INotificator _notificator;
    
    public ReadFactory(
        TodoService todoService, 
        ConsoleOutputSnippetsHolder consoleOutputSnippetsHolder, 
        INotificator notificator)
    {
        _todoService = todoService;
        _consoleOutputSnippetsHolder = consoleOutputSnippetsHolder;
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
                ShowTasksFromService(SortType.CreatedAt, -1);
                break;
            case "today":
                ShowTasksFromService(GetByDateOption.Today);
                break;
            case "yesterday":
                ShowTasksFromService(GetByDateOption.Yesterday);
                break;
            case "thisWeek":
                ShowTasksFromService(GetByDateOption.ThisWeek);
                break;
            case "lastWeek":
                ShowTasksFromService(GetByDateOption.LastWeek);
                break;
            case "thisMonth":
                ShowTasksFromService(GetByDateOption.ThisMonth);
                break;
            case "lastMonth":
                ShowTasksFromService(GetByDateOption.LastMonth);
                break;
            case "thisYear":
                ShowTasksFromService(GetByDateOption.ThisYear);
                break;
            case "lastYear":
                ShowTasksFromService(GetByDateOption.LastYear);
                break;
            default: _notificator.NotifyWarning("Wrong argument."); break;
        }
    }
    
    private void ShowTasksFromService(SortType sortOption, int order)
    {
        var response = _todoService.GetAll(sortOption, order);
        
        if (!response.IsSuccess)
        {
            _notificator.NotifyWarning(response.Message);
            return;
        }
        
        _consoleOutputSnippetsHolder.ShowTasks(response.Data!);
    }
    
    private void ShowTasksFromService(GetByDateOption dateOption)
    {
        var response = _todoService.GetAll(dateOption);
        
        if (!response.IsSuccess)
        {
            _notificator.NotifyWarning(response.Message);
            return;
        }
        
        _consoleOutputSnippetsHolder.ShowTasks(response.Data!);
    }
}