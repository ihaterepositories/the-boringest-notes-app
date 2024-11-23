namespace TheMostBoringNotesApp.Models;

public abstract class BaseModel
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    protected BaseModel(string content)
    {
        Id = Guid.NewGuid();
        Content = content;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
    
    public void Update(string content)
    {
        Content = content;
        UpdatedAt = DateTime.Now;
    }
}