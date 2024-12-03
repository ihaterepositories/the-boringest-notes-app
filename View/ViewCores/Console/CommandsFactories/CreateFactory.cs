using TheMostBoringNotesApp.Services;
using TheMostBoringNotesApp.View.ViewCores.Console.Snippets;

namespace TheMostBoringNotesApp.View.ViewCores.Console.CommandsFactories;

public class CreateFactory
{
    private readonly TaskService _taskService;
    private readonly OutputSnippetsHolder _outputSnippetsHolder;
    
    public CreateFactory(
        TaskService taskService, 
        OutputSnippetsHolder outputSnippetsHolder)
    {
        _taskService = taskService;
        _outputSnippetsHolder = outputSnippetsHolder;
    }
    
    public void Create()
    {
        var content = _outputSnippetsHolder.GetUserInput("Content: ");
        _taskService.Add(content);
    }
}