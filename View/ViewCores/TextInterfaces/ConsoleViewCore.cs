using TheMostBoringNotesApp.Services;
using TheMostBoringNotesApp.View.Interfaces;
using TheMostBoringNotesApp.View.Notifiers.Interfaces;
using TheMostBoringNotesApp.View.ViewCores.TextInterfaces.CommandsFactories;
using TheMostBoringNotesApp.View.ViewCores.TextInterfaces.Snippets;

namespace TheMostBoringNotesApp.View.ViewCores.TextInterfaces;

public class ConsoleViewCore : IViewCore
{
    private readonly OutputSnippetsHolder _outputSnippetsHolder;
    
    private readonly CreateFactory _createFactory;
    private readonly ReadFactory _readFactory;
    private readonly UpdateFactory _updateFactory;
    private readonly DeleteFactory _deleteFactory;

    private readonly SupportCommandsText _supportCommandsText;
    
    public ConsoleViewCore(TaskService taskService, INotificator notificator)
    {
        _outputSnippetsHolder = new OutputSnippetsHolder(notificator);
        
        _readFactory = new ReadFactory(taskService, _outputSnippetsHolder, notificator);
        _createFactory = new CreateFactory(taskService, _outputSnippetsHolder);
        _updateFactory = new UpdateFactory(taskService, _outputSnippetsHolder, notificator);
        _deleteFactory = new DeleteFactory(taskService, _outputSnippetsHolder, notificator);
    }
    
    public void Run()
    {
        _outputSnippetsHolder.ShowWelcomeMessage();
        var input = _outputSnippetsHolder.GetUserInput();

        while (input != "exit")
        {
            string[] inputParts = input.Split(' ');
            string command = inputParts[0];
            string[] args = inputParts.Skip(1).ToArray();
            
            ExecuteCommand(command, args);
            
            input = _outputSnippetsHolder.GetUserInput();
        }
    }
    
    private void ExecuteCommand(string command, string[] args)
    {
        switch (command)
        {
            case "create": _createFactory.Create(); break;
            case "read": _readFactory.Read(args); break;
            case "update": _updateFactory.Update(args); break;
            case "mark": _updateFactory.Mark(args); break;
            case "delete": _deleteFactory.Delete(args); break;
            case "help": Console.WriteLine(_supportCommandsText.CommandsTextList); break;
            default:
                _outputSnippetsHolder.ShowUnknownCommandMessage();
                break;
        }
    }
}