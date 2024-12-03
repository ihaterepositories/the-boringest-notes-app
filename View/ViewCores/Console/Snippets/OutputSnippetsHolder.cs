using ConsoleTables;
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
        System.Console.WriteLine("/ Welcome to The Most Boring Notes App!");
        System.Console.WriteLine("/ Type 'help' to see available commands.");
    }
    
    public void ShowUnknownCommandMessage()
    { 
        _notificator.NotifyWarning("Unknown command. Type 'help' to see available commands.");
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
            System.Console.WriteLine("/ "+question);
            System.Console.Write("> ");
            var input = System.Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) return input;
            _notificator.NotifyWarning("Input is empty");
        }
    }
    
    public bool ConfirmAction(string action)
    {
        System.Console.WriteLine($"/ Are you sure you want to {action}? (y/n)");
        return GetUserInput() == "y";
    }
    
    public void ShowTasks(List<Task> tasks)
    {
        if (tasks.Count == 0) return;
        var table = new ConsoleTable("N", "Done", "Content");
        foreach (var task in tasks)
            table.AddRow(task.Id, task.IsDone ? "*" : " ", task.Content);
        table.Write();
    }
}