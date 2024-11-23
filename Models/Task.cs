namespace TheMostBoringNotesApp.Models;

public class Task(string content) : BaseModel(content)
{
    public bool IsDone { get; set; } = false;
    
    public void ChangeStatus()
    {
        IsDone = !IsDone;
    }
}