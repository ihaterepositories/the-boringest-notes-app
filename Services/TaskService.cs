using TheMostBoringNotesApp.Repositories.Interfaces;
using TheMostBoringNotesApp.Services.Enums;
using TheMostBoringNotesApp.Services.Factories;
using TheMostBoringNotesApp.Services.Responding;
using TheMostBoringNotesApp.Services.Responding.Models;
using TheMostBoringNotesApp.Utils.Validators;
using TheMostBoringNotesApp.View.Notifiers.Interfaces;
using Task = TheMostBoringNotesApp.Models.Task;

namespace TheMostBoringNotesApp.Services;

public class TaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly TaskValidator _taskValidator;
    private readonly INotificator _notificator;
    private readonly ResponseCreator _responseCreator;
    
    public TaskService(
        ITaskRepository taskRepository, 
        TaskValidator taskValidator,
        INotificator notificator)
    {
        _taskRepository = taskRepository;
        _taskValidator = taskValidator;
        _notificator = notificator;
        
        _responseCreator = new ResponseCreator();
    }
    
    public List<Task> GetAll(TaskGetByDateOption dateOption)
    {
        var tasks = _taskRepository.GetAll();
        var dateFilter = TaskGetByDateFactory.GetByDate(dateOption);
        var sorter = TaskSorterFactory.GetSorter(TaskSortType.CreatedAt, -1);
        return sorter(dateFilter(tasks));
    }
    
    public List<Task> GetAll(TaskSortType sortType, int sortOrder, int limit = 0)
    {
        var tasks = _taskRepository.GetAll();
        var sorter = TaskSorterFactory.GetSorter(sortType, sortOrder);
        return limit == 0 ? sorter(tasks) : sorter(tasks).Take(limit).ToList();
    }
    
    public ServiceResponse<Task> GetById(int id)
    {
        var task = _taskRepository.GetById(id);
        
        if (task == Task.Empty)
            return _responseCreator.CreateError<Task>($"Task {id} not found");
        
        return _responseCreator.CreateOk(task);
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