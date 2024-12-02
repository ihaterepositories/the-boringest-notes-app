using TheMostBoringNotesApp.Services;
using TheMostBoringNotesApp.Utils.Notifiers.Interfaces;
using TheMostBoringNotesApp.View.Interfaces;
using TheMostBoringNotesApp.View.ViewCores.Console.CommandFactories;
using TheMostBoringNotesApp.View.ViewCores.Console.Snippets;

namespace TheMostBoringNotesApp.View.ViewCores.Console;

public class ConsoleViewCore : IViewCore
{
    private readonly TaskCommandsExecuteFactory _taskCommandsExecuteFactory;
    private readonly OutputSnippetsHolder _outputSnippetsHolder;
    
    public ConsoleViewCore(TaskService taskService, INotificator notificator)
    {
        _outputSnippetsHolder = new OutputSnippetsHolder(notificator);
        _taskCommandsExecuteFactory = new TaskCommandsExecuteFactory(
            taskService, 
            _outputSnippetsHolder,
            notificator
            );
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
            
            _taskCommandsExecuteFactory.Execute(command, args);
            
            input = _outputSnippetsHolder.GetUserInput();
        }
    }
}