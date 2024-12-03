namespace TheMostBoringNotesApp.Models;

public class Task(int id, string content) : BaseModel(id, content)
{
    public bool IsDone { get; set; } = false;
    
    public void ChangeStatus()
    {
        IsDone = !IsDone;
    }
}