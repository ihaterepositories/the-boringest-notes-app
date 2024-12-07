using System.Text.Json;
using TodoAPI.Models;
using TodoAPI.Repositories.Interfaces;

namespace TodoAPI.Repositories;

public class LocalTodoRepository : ITodoRepository
{
    private readonly string _localStoragePath;
    private readonly string _repositoryObjectName;
    
    private readonly List<Todo> _todos;

    public LocalTodoRepository(
        string localStoragePath, 
        string repositoryObjectName)
    {
        _localStoragePath = localStoragePath;
        _repositoryObjectName = repositoryObjectName;
        
        _todos = LoadEntitiesFromFile();
    }
    
    public int GetNextId()
    {
        return _todos.Count == 0 ? 1 : _todos.Max(n => n.Id) + 1;
    }
    
    public bool IsIdExists(int id)
    {
        return _todos.Any(n => n.Id == id);
    }
    
    public List<Todo> GetAll()
    {
        return _todos;
    }

    public Todo GetById(int id)
    {
        return _todos.FirstOrDefault(n => n.Id == id) ?? Todo.Empty;
    }

    public void Add(Todo entity)
    {
        _todos.Add(entity);
        SaveEntitiesToFile();
    }

    public void Update(Todo entity)
    {
        _todos
            .FirstOrDefault(n => n.Id == entity.Id)!
            .Update(entity.Content);
        
        SaveEntitiesToFile();
    }

    public void Delete(int id)
    {
        _todos.Remove(_todos.FirstOrDefault(e => e.Id == id)!);
        SaveEntitiesToFile();
    }
    
    public void DeleteAll()
    {
        _todos.Clear();
        SaveEntitiesToFile();
    }
    
    private List<Todo> LoadEntitiesFromFile()
    {
        if (!File.Exists(_localStoragePath))
        {
            Console.WriteLine($"Can`t load {_repositoryObjectName}s, file not found at {_localStoragePath}");
            return [];
        }

        var json = File.ReadAllText(_localStoragePath);
        return JsonSerializer.Deserialize<List<Todo>>(json) ?? [];
    }

    private void SaveEntitiesToFile()
    {
        try
        {
            var json = JsonSerializer.Serialize(_todos);
            File.WriteAllText(_localStoragePath, json);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Can`t save {_repositoryObjectName}s to file: {e.Message}");
        }
    }
}