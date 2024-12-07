using TodoAPI.Services;
using TodoView.ViewCores.TextInterfaceCores.Notifying.Interfaces;
using TodoView.ViewCores.TextInterfaceCores.Snippets;

namespace TodoView.ViewCores.TextInterfaceCores.CommandsFactories;

public class CreateFactory
{
    private readonly TodoService _todoService;
    private readonly ConsoleOutputSnippetsHolder _consoleOutputSnippetsHolder;
    private readonly INotificator _notificator;
    
    public CreateFactory(
        TodoService todoService, 
        ConsoleOutputSnippetsHolder consoleOutputSnippetsHolder,
        INotificator notificator)
    {
        _todoService = todoService;
        _consoleOutputSnippetsHolder = consoleOutputSnippetsHolder;
        _notificator = notificator;
    }
    
    public void Create()
    {
        var content = _consoleOutputSnippetsHolder.GetUserInput("Content: ");
        var response = _todoService.Add(content);
        _notificator.Notify(response.Message);
    }
}