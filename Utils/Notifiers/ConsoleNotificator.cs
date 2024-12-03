using TheMostBoringNotesApp.Utils.Notifiers.Interfaces;

namespace TheMostBoringNotesApp.Utils.Notifiers;

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