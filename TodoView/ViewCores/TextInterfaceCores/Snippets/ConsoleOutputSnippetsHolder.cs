using ConsoleTables;
using TodoAPI.Models;
using TodoView.ViewCores.TextInterfaceCores.Notifying.Interfaces;

namespace TodoView.ViewCores.TextInterfaceCores.Snippets;

public class ConsoleOutputSnippetsHolder
{
    private readonly INotificator _notificator;
    
    public ConsoleOutputSnippetsHolder(INotificator notificator)
    {
        _notificator = notificator;
    }

    public void ShowWelcomeMessage()
    {
        Console.WriteLine("/ Welcome to The Most Boring Notes App!");
        Console.WriteLine("/ Type 'help' to see available commands.");
    }
    
    public void ShowUnknownCommandMessage()
    { 
        _notificator.NotifyWarning(
            "Unknown command. Type 'help' to see available commands.\n"
        );
    }
    
    public string GetUserInput()
    {
        while (true)
        {
            Console.Write("> ");
            var input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) return input;
            _notificator.NotifyWarning("Input is empty");
        }
    }
    
    public string GetUserInput(string question)
    {
        while (true)
        {
            Console.WriteLine("/ "+question);
            Console.Write("> ");
            var input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) return input;
            _notificator.NotifyWarning("Input is empty");
        }
    }
    
    public bool ConfirmAction(string action)
    {
        Console.WriteLine($"/ Are you sure you want to {action}? (y/n)");
        return GetUserInput() == "y";
    }
    
    public void ShowTasks(List<Todo> tasks)
    {
        if (tasks.Count == 0) return;
        
        var table = new ConsoleTable("ID", "Done", "Content");
        
        foreach (var task in tasks)
            table.AddRow(task.Id, task.IsDone ? "*" : " ", task.Content);
        
        table.Options.EnableCount = false;
        
        Console.WriteLine();
        table.Write();
    }
}