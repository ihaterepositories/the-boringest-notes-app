namespace TheMostBoringNotesApp.Models;

public class Note
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public Note(string title, string content)
    {
        Id = Guid.NewGuid();
        Content = content;
        IsDone = false;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
    
    public void Update(string content)
    {
        Content = content;
        UpdatedAt = DateTime.Now;
    }
}