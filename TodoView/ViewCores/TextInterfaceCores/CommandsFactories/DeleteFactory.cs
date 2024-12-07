using TodoAPI.Services;
using TodoView.ViewCores.TextInterfaceCores.Notifying.Interfaces;
using TodoView.ViewCores.TextInterfaceCores.Snippets;

namespace TodoView.ViewCores.TextInterfaceCores.CommandsFactories;

public class DeleteFactory
{
    private readonly TodoService _todoService;
    private readonly ConsoleOutputSnippetsHolder _consoleOutputSnippetsHolder;
    private readonly INotificator _notificator;
    
    public DeleteFactory(
        TodoService todoService, 
        ConsoleOutputSnippetsHolder consoleOutputSnippetsHolder, 
        INotificator notificator)
    {
        _todoService = todoService;
        _consoleOutputSnippetsHolder = consoleOutputSnippetsHolder;
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
            var response = _todoService.Delete(id);
            _notificator.Notify(response.Message);
        }
        else
        {
            switch (args[0])
            {
                case "all":
                    if (_consoleOutputSnippetsHolder.ConfirmAction("delete all todos"))
                    {
                        var response = _todoService.DeleteAll();
                        _notificator.Notify(response.Message);
                    }
                    break;
            }
        }
    }
}