using TheMostBoringNotesApp.Repositories.Interfaces;
using TheMostBoringNotesApp.Services.Enums;
using TheMostBoringNotesApp.Services.Factories;
using TheMostBoringNotesApp.Utils.Notifiers.Interfaces;
using TheMostBoringNotesApp.Utils.Validators;
using Task = TheMostBoringNotesApp.Models.Task;

namespace TheMostBoringNotesApp.Services;

public class TaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly TaskValidator _taskValidator;
    private readonly INotificator _notificator;
    
    public TaskService(
        ITaskRepository taskRepository, 
        TaskValidator taskValidator,
        INotificator notificator)
    {
        _taskRepository = taskRepository;
        _taskValidator = taskValidator;
        _notificator = notificator;
    }
    
    public List<Task> GetByDate(TaskGetByDateOption dateOption)
    {
        var tasks = _taskRepository.GetAll();
        var dateFilter = TaskGetByDateFactory.GetByDate(dateOption);
        return dateFilter(tasks);
    }
    
    public List<Task> GetAll(TaskSortType sortType, int sortOrder, int limit = 0)
    {
        var tasks = _taskRepository.GetAll();
        var sorter = TaskSorterFactory.GetSorter(sortType, 1);
        return limit == 0 ? sorter(tasks) : sorter(tasks).Take(limit).ToList();
    }
    
    public Task GetById(int id)
    {
        return _taskRepository.GetById(id);
    }
    
    public void Add(string content)
    {
        var task = new Task(_taskRepository.GetNextId(), content);
        _taskValidator.Validate(task);
        _taskRepository.Add(task);
    }
    
    public void UpdateContent(int id, string content)
    {
        if (!_taskRepository.IsIdExists(id))
            _notificator.NotifyWarning($"Task {id} not found");
        
        _taskRepository.GetById(id).Update(content);
        _notificator.Notify($"Task {id} updated");
    }
    
    public void Delete(int id)
    {
        if (!_taskRepository.IsIdExists(id))
            _notificator.NotifyWarning($"Task {id} not found");
        
        _taskRepository.Delete(id);
        _notificator.Notify($"Task {id} deleted");
    }
    
    public void DeleteAll()
    {
        _taskRepository.DeleteAll();
        _notificator.Notify("All tasks deleted");
    }
}