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
    
    public List<T> GetAll()
    {
        if (_entities.Count == 0)
            _notificator.NotifyWarning($"No {_repositoryObjectName} found");
        
        return _entities;
    }

    public T GetById(Guid id)
    {
        var entity = _entities.FirstOrDefault(n => n.Id == id);
        
        if (entity == null)
        {
            _notificator.NotifyWarning($"{_repositoryObjectName} with id {id} not found");
        }
        
        return entity;
    }

    public void Add(T entity)
    {
        _entities.Add(entity);
        SaveNotesToFile();
    }

    public void Update(T entity)
    {
        var existingEntity = _entities.FirstOrDefault(n => n.Id == entity.Id);
        
        if (existingEntity == null)
        {
            _notificator.NotifyWarning($"Can`t update {_repositoryObjectName} with id {entity.Id}, {_repositoryObjectName} not found");
            return;
        }
        
        existingEntity.Update(entity.Content);
        
        SaveNotesToFile();
    }

    public void Delete(Guid id)
    {
        var entity = _entities.FirstOrDefault(n => n.Id == id);
        
        if (entity == null)
        {
            _notificator.NotifyWarning($"Can`t delete {_repositoryObjectName} with id {id}, {_repositoryObjectName} not found");
            return;
        }
        
        _entities.Remove(entity);
        SaveNotesToFile();
    }
    
    private List<T> LoadEntitiesFromFile()
    {
        if (!File.Exists(_localStoragePath))
        {
            _notificator.NotifyWarning($"Can`t load {_repositoryObjectName}s, file not found at {_localStoragePath}");
            return [];
        }

        var json = File.ReadAllText(_localStoragePath);
        _notificator.Notify($"{_repositoryObjectName}s loaded");
        return JsonSerializer.Deserialize<List<T>>(json) ?? [];
    }

    private void SaveNotesToFile()
    {
        try
        {
            var json = JsonSerializer.Serialize(_entities);
            File.WriteAllText(_localStoragePath, json);
            _notificator.Notify($"{_repositoryObjectName}s saved");
        }
        catch (Exception e)
        {
            _notificator.NotifyError($"Can`t save {_repositoryObjectName}s to file: {e.Message}");
        }
    }
}