using TheMostBoringNotesApp.Utils.Notifiers.Interfaces;
using Task = TheMostBoringNotesApp.Models.Task;

namespace TheMostBoringNotesApp.View.ViewCores.Console.Snippets;

public class OutputSnippetsHolder
{
    private readonly INotificator _notificator;
    
    public OutputSnippetsHolder(INotificator notificator)
    {
        _notificator = notificator;
    }
    
    public const string BoldLine = "========================================";
    public const string Line = "----------------------------------------";

    public void ShowWelcomeMessage()
    {
        System.Console.WriteLine("Welcome to The Most Boring Notes App!");
        System.Console.WriteLine("Type 'help' to see available commands.");
    }
    
    public void ShowUnknownCommandMessage()
    {
        System.Console.WriteLine("Unknown command. Type 'help' to see available commands.");
    }
    
    public string GetUserInput()
    {
        while (true)
        {
            System.Console.Write("> ");
            var input = System.Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) return input;
            _notificator.NotifyWarning("Input is empty");
        }
    }
    
    public string GetUserInput(string question)
    {
        while (true)
        {
            System.Console.WriteLine(question);
            System.Console.Write("> ");
            var input = System.Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) return input;
            _notificator.NotifyWarning("Input is empty");
        }
    }
    
    public bool ConfirmAction(string action)
    {
        System.Console.WriteLine($"Are you sure you want to {action}? (y/n)");
        System.Console.Write("> ");
        var confirmation = System.Console.ReadLine();
        if (confirmation != "y")
        {
            _notificator.Notify("Action cancelled");
            return false;
        }
        return true;
    }
    
    public void ShowTasks(List<Task> tasks)
    {
        if (tasks.Count == 0) return;
        foreach (var task in tasks)
        {
            System.Console.WriteLine(Line);
            System.Console.Write(task.IsDone ? "(*)" : "( )");
            System.Console.WriteLine(task.Content);
        }
        System.Console.WriteLine(Line);
    }
}