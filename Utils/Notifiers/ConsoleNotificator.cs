using TheMostBoringNotesApp.Utils.Notifiers.Interfaces;

namespace TheMostBoringNotesApp.Utils.Notifiers;

public class ConsoleNotificator : INotificator
{
    public void Notify(string message)
    {
        Console.WriteLine($"/ {message}");
    }
    
    public void NotifyWarning(string message)
    {
        Console.WriteLine($"/ WARNING: {message}");
    }
    
    public void NotifyError(string message)
    {
        Console.WriteLine($"/ ERROR: {message}");
    }
}