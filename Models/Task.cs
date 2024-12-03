namespace TheMostBoringNotesApp.Models;

public class Task
{
    public int Id { get; set; }
    public string Content { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Task(int id, string content)
    {
        Id = id;
        Content = content;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
    
    public void Update(string content)
    {
        Content = content;
        UpdatedAt = DateTime.Now;
    }
    
    public void ChangeStatus()
    {
        IsDone = !IsDone;
    }
    
    public static Task Empty => new(0, string.Empty);
}