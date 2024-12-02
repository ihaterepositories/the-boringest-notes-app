using TheMostBoringNotesApp.Repositories.Interfaces;
using TheMostBoringNotesApp.Services.Enums;
using TheMostBoringNotesApp.Services.Factories;
using TheMostBoringNotesApp.Utils.Validators;
using Task = TheMostBoringNotesApp.Models.Task;

namespace TheMostBoringNotesApp.Services;

public class TaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly TaskValidator _taskValidator;
    
    public TaskService(ITaskRepository taskRepository, TaskValidator taskValidator)
    {
        _taskRepository = taskRepository;
        _taskValidator = taskValidator;
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
    
    public Task GetById(Guid id)
    {
        return _taskRepository.GetById(id);
    }
    
    public void Add(string content)
    {
        var task = new Task(content);
        _taskValidator.Validate(task);
        _taskRepository.Add(task);
    }
    
    public void Update(Task note)
    {
        _taskValidator.Validate(note);
        _taskRepository.Update(note);
    }
    
    public void Delete(Guid id)
    {
        _taskRepository.Delete(id);
    }
}