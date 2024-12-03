using System.Text.Json;
using TheMostBoringNotesApp.Models;
using TheMostBoringNotesApp.Utils.Notifiers.Interfaces;

namespace TheMostBoringNotesApp.Repositories.Interfaces;

public abstract class LocalGenericRepository<T> where T : BaseModel
{
    private readonly string _localStoragePath;
    private readonly INotificator _notificator;
    private readonly string _repositoryObjectName;
    
    private readonly List<T> _entities;

    protected LocalGenericRepository(
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
    
    public List<T> GetAll()
    {
        return _entities;
    }

    public T? GetById(int id)
    {
        return _entities.FirstOrDefault(e => e.Id == id);
    }

    public void Add(T entity)
    {
        _entities.Add(entity);
        SaveEntitiesToFile();
    }

    public void Update(T entity)
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
    
    private List<T> LoadEntitiesFromFile()
    {
        if (!File.Exists(_localStoragePath))
        {
            _notificator.NotifyWarning($"Can`t load {_repositoryObjectName}s, file not found at {_localStoragePath}");
            return [];
        }

        var json = File.ReadAllText(_localStoragePath);
        return JsonSerializer.Deserialize<List<T>>(json) ?? [];
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