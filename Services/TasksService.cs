using TheMostBoringNotesApp.Repositories.Interfaces;
using TheMostBoringNotesApp.Services.Enums;
using TheMostBoringNotesApp.Services.Factories;
using TheMostBoringNotesApp.Utils;
using TheMostBoringNotesApp.Utils.Validators;
using Task = TheMostBoringNotesApp.Models.Task;

namespace TheMostBoringNotesApp.Services;

public class TasksService(ITasksRepository tasksRepository)
{
    private readonly Logger _logger = new Logger("TasksService");
    private readonly TaskValidator _noteValidator = new TaskValidator();
    
    public List<Task> GetToday()
    {
        var tasks = tasksRepository.GetAll();
        return tasks.Where(n => n.CreatedAt.Date == DateTime.Today).ToList();
    }
    
    public List<Task> GetAll(TasksSortType sortType, int sortOrder, int limit = 0)
    {
        var tasks = tasksRepository.GetAll();
        var sorter = TasksSorterFactory.GetSorter(sortType, 1);
        return limit == 0 ? sorter(tasks) : sorter(tasks).Take(limit).ToList();
    }
    
    public Task GetById(Guid id)
    {
        return tasksRepository.GetById(id);
    }
    
    public void Add(Task note)
    {
        _noteValidator.Validate(note);
        tasksRepository.Add(note);
    }
    
    public void Update(Task note)
    {
        _noteValidator.Validate(note);
        tasksRepository.Update(note);
    }
    
    public void Delete(Guid id)
    {
        tasksRepository.Delete(id);
    }
}