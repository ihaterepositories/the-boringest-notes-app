using System.Globalization;
using TodoAPI.Services;
using TodoView.ViewCores.TextInterfaceCores.Notifying.Interfaces;
using TodoView.ViewCores.TextInterfaceCores.Snippets;

namespace TodoView.ViewCores.TextInterfaceCores.CommandsFactories;

public class UpdateFactory
{
    private readonly TodoService _todoService;
    private readonly INotificator _notificator;
    private readonly ConsoleOutputSnippetsHolder _consoleOutputSnippetsHolder;
    
    public UpdateFactory(
        TodoService todoService,
        ConsoleOutputSnippetsHolder consoleOutputSnippetsHolder,
        INotificator notificator)
    {
        _todoService = todoService;
        _consoleOutputSnippetsHolder = consoleOutputSnippetsHolder;
        _notificator = notificator;
    }
    
    public void Update(string[] args)
    {
        if (args.Length == 0)
        {
            _notificator.NotifyWarning("Provide todo id");
            return;
        }
        
        if (int.TryParse(args[0], out var id))
        {
            var content = _consoleOutputSnippetsHolder.GetUserInput("New content:");
            var response = _todoService.UpdateContent(id, content);
            _notificator.Notify(response.Message);
        }
        else
        {
            _notificator.NotifyWarning("Provide valid todo id");
        }
    }

    public void Mark(string[] args)
    {
        if (args.Length == 0)
        {
            _notificator.NotifyWarning("Provide todo id");
            return;
        }

        if (args.Length == 1 && int.TryParse(args[0], out var id))
        {
            var response = _todoService.GetById(id);

            if (!response.IsSuccess)
            {
                _notificator.NotifyWarning(response.Message);
                return;
            }

            var task = response.Data!;
            task.ChangeStatus();
            _notificator.Notify("Todo marked as " + (task.IsDone ? "done" : "undone"));
        }
        else
        {
            _notificator.NotifyWarning("Provide valid todo id");
        }
    }
}