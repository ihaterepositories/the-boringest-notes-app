using Task = TheMostBoringNotesApp.Models.Task;

namespace TheMostBoringNotesApp.Repositories.Interfaces;

public interface ITaskRepository
{
    public int GetNextId();
    public bool IsIdExists(int id);
    public List<Task> GetAll();
    public Task GetById(int id);
    public void Add(Task note);
    public void Update(Task note);
    public void Delete(int id);
    public void DeleteAll();
}