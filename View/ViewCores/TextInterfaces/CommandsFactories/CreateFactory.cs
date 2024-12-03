using TheMostBoringNotesApp.Services;
using TheMostBoringNotesApp.View.ViewCores.TextInterfaces.Snippets;

namespace TheMostBoringNotesApp.View.ViewCores.TextInterfaces.CommandsFactories;

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