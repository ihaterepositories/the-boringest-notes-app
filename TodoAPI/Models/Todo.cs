namespace TodoAPI.Models;

public class Todo
{
    public int Id { get; set; }
    public string Content { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Todo(int id, string content)
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
    
    public static Todo Empty => new(0, string.Empty);
}