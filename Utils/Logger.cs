namespace TheMostBoringNotesApp.Utils;

public class Logger(string name)
{
    public void Log(string message)
    {
        Console.WriteLine($"> [{name}] {message}");
    }
    
    public void LogError(string message)
    {
        Console.WriteLine($"> [{name}] ERROR: {message}");
    }
    
    public void LogWarning(string message)
    {
        Console.WriteLine($"> [{name}] WARNING: {message}");
    }
}