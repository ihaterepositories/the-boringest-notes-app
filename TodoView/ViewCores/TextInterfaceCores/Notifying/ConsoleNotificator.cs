using TodoView.ViewCores.TextInterfaceCores.Notifying.Interfaces;

namespace TodoView.ViewCores.TextInterfaceCores.Notifying;

public class ConsoleNotificator : INotificator
{
    public void Notify(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"/ {message}");
        Console.ResetColor();
    }
    
    public void NotifyWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"/ WARNING: {message}");
        Console.ResetColor();
    }
    
    public void NotifyError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"/ ERROR: {message}");
        Console.ResetColor();
    }
}