using TodoAPI.Models;

namespace TodoAPI.Repositories.Interfaces;

public interface ITodoRepository
{
    public int GetNextId();
    public bool IsIdExists(int id);
    public List<Todo> GetAll();
    public Todo GetById(int id);
    public void Add(Todo note);
    public void Update(Todo note);
    public void Delete(int id);
    public void DeleteAll();
}