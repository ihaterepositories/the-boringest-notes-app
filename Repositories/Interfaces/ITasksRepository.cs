using Task = TheMostBoringNotesApp.Models.Task;

namespace TheMostBoringNotesApp.Repositories.Interfaces;

public interface ITasksRepository
{
    public List<Task> GetAll();
    public Task GetById(Guid id);
    public void Add(Task note);
    public void Update(Task note);
    public void Delete(Guid id);
}