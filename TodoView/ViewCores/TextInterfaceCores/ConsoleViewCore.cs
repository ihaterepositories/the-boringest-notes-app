using TodoAPI.Services;
using TodoView.Interfaces;
using TodoView.ViewCores.TextInterfaceCores.CommandsFactories;
using TodoView.ViewCores.TextInterfaceCores.Notifying.Interfaces;
using TodoView.ViewCores.TextInterfaceCores.Snippets;

namespace TodoView.ViewCores.TextInterfaceCores;

public class ConsoleViewCore : IViewCore
{
    private readonly ConsoleOutputSnippetsHolder _consoleOutputSnippetsHolder;
    private readonly INotificator _notificator;
    
    private readonly CreateFactory _createFactory;
    private readonly ReadFactory _readFactory;
    private readonly UpdateFactory _updateFactory;
    private readonly DeleteFactory _deleteFactory;

    private readonly SupportCommandsText _supportCommandsText;
    
    public ConsoleViewCore(TodoService todoService, INotificator notificator)
    {
        _notificator = notificator;
        
        _supportCommandsText = new SupportCommandsText();
        _consoleOutputSnippetsHolder = new ConsoleOutputSnippetsHolder(notificator);
        
        _readFactory = new ReadFactory(todoService, _consoleOutputSnippetsHolder, notificator);
        _createFactory = new CreateFactory(todoService, _consoleOutputSnippetsHolder, notificator);
        _updateFactory = new UpdateFactory(todoService, _consoleOutputSnippetsHolder, notificator);
        _deleteFactory = new DeleteFactory(todoService, _consoleOutputSnippetsHolder, notificator);
    }
    
    public void Run()
    {
        _consoleOutputSnippetsHolder.ShowWelcomeMessage();
        var input = _consoleOutputSnippetsHolder.GetUserInput();

        while (input != "exit")
        {
            string[] inputParts = input.Split(' ');
            string command = inputParts[0];
            string[] args = inputParts.Skip(1).ToArray();
            
            ExecuteCommand(command, args);
            
            input = _consoleOutputSnippetsHolder.GetUserInput();
        }
    }
    
    private void ExecuteCommand(string command, string[] args)
    {
        if (string.IsNullOrEmpty(command))
        {
            _notificator.NotifyWarning("Provide command.");
            return;
        }
        
        if (command != "create" && 
            command != "help" && 
            args.Length == 0 &&
            command is "read" or "update" or "mark" or "delete")
        {
            _notificator.NotifyWarning("Provide argument.");
            return;
        }
        
        switch (command)
        {
            case "create": _createFactory.Create(); break;
            case "read": _readFactory.Read(args); break;
            case "update": _updateFactory.Update(args); break;
            case "mark": _updateFactory.Mark(args); break;
            case "delete": _deleteFactory.Delete(args); break;
            case "help": Console.WriteLine(_supportCommandsText.CommandsTextList); break;
            default:
                _consoleOutputSnippetsHolder.ShowUnknownCommandMessage();
                break;
        }
    }
}