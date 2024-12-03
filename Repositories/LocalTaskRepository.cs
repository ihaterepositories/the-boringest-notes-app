using System.Text.Json;
using TheMostBoringNotesApp.Repositories.Interfaces;
using TheMostBoringNotesApp.View.Notifiers.Interfaces;
using Task = TheMostBoringNotesApp.Models.Task;

namespace TheMostBoringNotesApp.Repositories;

public class LocalTaskRepository : ITaskRepository
{
    private readonly string _localStoragePath;
    private readonly INotificator _notificator;
    private readonly string _repositoryObjectName;
    
    private readonly List<Task> _entities;

    public LocalTaskRepository(
        string localStoragePath, 
        INotificator notificator,
        string repositoryObjectName)
    {
        _localStoragePath = localStoragePath;
        _notificator = notificator;
        _repositoryObjectName = repositoryObjectName;
        
        _entities = LoadEntitiesFromFile();
    }
    
    public int GetNextId()
    {
        return _entities.Count == 0 ? 1 : _entities.Max(n => n.Id) + 1;
    }
    
    public bool IsIdExists(int id)
    {
        return _entities.Any(n => n.Id == id);
    }
    
    public List<Task> GetAll()
    {
        return _entities;
    }

    public Task GetById(int id)
    {
        return _entities.FirstOrDefault(n => n.Id == id) ?? Task.Empty;
    }

    public void Add(Task entity)
    {
        _entities.Add(entity);
        SaveEntitiesToFile();
    }

    public void Update(Task entity)
    {
        _entities
            .FirstOrDefault(n => n.Id == entity.Id)!
            .Update(entity.Content);
        
        SaveEntitiesToFile();
    }

    public void Delete(int id)
    {
        _entities.Remove(_entities.FirstOrDefault(e => e.Id == id)!);
        SaveEntitiesToFile();
    }
    
    public void DeleteAll()
    {
        _entities.Clear();
        SaveEntitiesToFile();
    }
    
    private List<Task> LoadEntitiesFromFile()
    {
        if (!File.Exists(_localStoragePath))
        {
            _notificator.NotifyWarning($"Can`t load {_repositoryObjectName}s, file not found at {_localStoragePath}");
            return [];
        }

        var json = File.ReadAllText(_localStoragePath);
        return JsonSerializer.Deserialize<List<Task>>(json) ?? [];
    }

    private void SaveEntitiesToFile()
    {
        try
        {
            var json = JsonSerializer.Serialize(_entities);
            File.WriteAllText(_localStoragePath, json);
        }
        catch (Exception e)
        {
            _notificator.NotifyError($"Can`t save {_repositoryObjectName}s to file: {e.Message}");
        }
    }
}