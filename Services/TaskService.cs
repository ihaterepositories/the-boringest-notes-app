using TheMostBoringNotesApp.Repositories.Interfaces;
using TheMostBoringNotesApp.Services.Enums;
using TheMostBoringNotesApp.Services.Factories;
using TheMostBoringNotesApp.Services.Responding;
using TheMostBoringNotesApp.Services.Responding.Models;
using TheMostBoringNotesApp.Utils.Validators;
using Task = TheMostBoringNotesApp.Models.Task;

namespace TheMostBoringNotesApp.Services;

public class TaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly TaskValidator _taskValidator;
    private readonly ResponseCreator _responseCreator;
    
    public TaskService(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
        
        _taskValidator = new TaskValidator();
        _responseCreator = new ResponseCreator();
    }
    
    public ServiceResponse<List<Task>> GetAll(TaskGetByDateOption dateOption)
    {
        try
        {
            var tasks = _taskRepository.GetAll();
        
            if (tasks.Count == 0)
                return _responseCreator.CreateError<List<Task>>("No tasks found");
            
            var dateFilter = TaskGetByDateFactory.GetByDate(dateOption);
            return _responseCreator.CreateOk(dateFilter(tasks));
        }
        catch (Exception e)
        {
            return _responseCreator.CreateError<List<Task>>(e.Message);
        }
    }
    
    public ServiceResponse<List<Task>> GetAll(TaskSortType sortType, int sortOrder, int limit = 0)
    {
        try
        {
            var tasks = _taskRepository.GetAll();
            
            if (tasks.Count == 0)
                return _responseCreator.CreateError<List<Task>>("No tasks found");
            
            var sorter = TaskSorterFactory.GetSorter(sortType, sortOrder);
            return limit == 0 ? _responseCreator.CreateOk(sorter(tasks)) : _responseCreator.CreateOk(sorter(tasks).Take(limit).ToList());
        }
        catch (Exception e)
        {
            return _responseCreator.CreateError<List<Task>>(e.Message);
        }
    }
    
    public ServiceResponse<Task> GetById(int id)
    {
        if (_taskRepository.IsIdExists(id))
            return _responseCreator.CreateError<Task>($"Task {id} not found");
        
        try
        {
            var task = _taskRepository.GetById(id);
        
            if (task == Task.Empty)
                return _responseCreator.CreateError<Task>($"Task {id} not found");
            
            return _responseCreator.CreateOk(task);
        }
        catch (Exception e)
        {
            return _responseCreator.CreateError<Task>(e.Message);
        }
    }
    
    public ServiceResponse<Task> Add(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            return _responseCreator.CreateError<Task>("Content cannot be empty");
        
        var task = new Task(_taskRepository.GetNextId(), content);
        
        if(!_taskValidator.Validate(task))
            return _responseCreator.CreateError<Task>(_taskValidator.ErrorMessage);
        
        try
        {
            _taskRepository.Add(task);
            return _responseCreator.CreateOk(task);
        }
        catch (Exception e)
        {
            return _responseCreator.CreateError<Task>(e.Message);
        }
    }
    
    public ServiceResponse<Task> UpdateContent(int id, string content)
    {
        if (!_taskRepository.IsIdExists(id))
            return _responseCreator.CreateError<Task>($"Task {id} not found");
        
        var updatedTask = new Task(id, content);
        
        if (!_taskValidator.Validate(updatedTask))
            return _responseCreator.CreateError<Task>(_taskValidator.ErrorMessage);
        
        try
        {
            _taskRepository.Update(updatedTask);
            return _responseCreator.CreateOk(updatedTask);
        }
        catch (Exception e)
        {
            return _responseCreator.CreateError<Task>(e.Message);
        }
    }
    
    public ServiceResponse<int> Delete(int id)
    {
        if (!_taskRepository.IsIdExists(id))
            return _responseCreator.CreateError<int>($"Task {id} not found");
        
        try
        {
            _taskRepository.Delete(id);
            return _responseCreator.CreateOk(id);
        }
        catch (Exception e)
        {
            return _responseCreator.CreateError<int>(e.Message);
        }
    }
    
    public ServiceResponse<string> DeleteAll()
    {
        try
        {
            _taskRepository.DeleteAll();
            return _responseCreator.CreateOk(string.Empty);
        }
        catch (Exception e)
        {
            return _responseCreator.CreateError<string>(e.Message);
        }
    }
}