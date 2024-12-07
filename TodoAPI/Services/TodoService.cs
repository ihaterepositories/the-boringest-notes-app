using TodoAPI.Models;
using TodoAPI.Repositories.Interfaces;
using TodoAPI.Services.Enums;
using TodoAPI.Services.Factories;
using TodoAPI.Services.Responding;
using TodoAPI.Services.Responding.Models;
using TodoAPI.Services.Validating;

namespace TodoAPI.Services;

public class TodoService
{
    private readonly ITodoRepository _todoRepository;
    private readonly TodoValidator _todoValidator;
    private readonly ResponseCreator _responseCreator;
    
    public TodoService(
        ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
        
        _todoValidator = new TodoValidator();
        _responseCreator = new ResponseCreator();
    }
    
    public ServiceResponse<List<Todo>> GetAll(GetByDateOption dateOption)
    {
        try
        {
            var todos = _todoRepository.GetAll();
        
            if (todos.Count == 0)
                return _responseCreator.CreateError<List<Todo>>("No todos found");
            
            var dateFilter = GetByDateFactory.GetByDate(dateOption);
            return _responseCreator.CreateOk(dateFilter(todos));
        }
        catch (Exception e)
        {
            return _responseCreator.CreateError<List<Todo>>(e.Message);
        }
    }
    
    public ServiceResponse<List<Todo>> GetAll(SortType sortType, int sortOrder, int limit = 0)
    {
        try
        {
            var todos = _todoRepository.GetAll();
            
            if (todos.Count == 0)
                return _responseCreator.CreateError<List<Todo>>("No todos found");
            
            var sorter = SortFactory.GetSorter(sortType, sortOrder);
            return limit == 0 ? _responseCreator.CreateOk(sorter(todos)) : _responseCreator.CreateOk(sorter(todos).Take(limit).ToList());
        }
        catch (Exception e)
        {
            return _responseCreator.CreateError<List<Todo>>(e.Message);
        }
    }
    
    public ServiceResponse<Todo> GetById(int id)
    {
        if (!_todoRepository.IsIdExists(id))
            return _responseCreator.CreateError<Todo>($"Todo {id} not found");
        
        try
        {
            var todo = _todoRepository.GetById(id);
            return _responseCreator.CreateOk(todo);
        }
        catch (Exception e)
        {
            return _responseCreator.CreateError<Todo>(e.Message);
        }
    }
    
    public ServiceResponse<Todo> Add(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            return _responseCreator.CreateError<Todo>("Content cannot be empty");
        
        var todo = new Todo(_todoRepository.GetNextId(), content);
        
        if(!_todoValidator.Validate(todo))
            return _responseCreator.CreateError<Todo>(_todoValidator.ErrorMessage);
        
        try
        {
            _todoRepository.Add(todo);
            return _responseCreator.CreateOk(todo);
        }
        catch (Exception e)
        {
            return _responseCreator.CreateError<Todo>(e.Message);
        }
    }
    
    public ServiceResponse<Todo> UpdateContent(int id, string content)
    {
        if (!_todoRepository.IsIdExists(id))
            return _responseCreator.CreateError<Todo>($"Todo {id} not found");
        
        var updatedTodo = new Todo(id, content);
        
        if (!_todoValidator.Validate(updatedTodo))
            return _responseCreator.CreateError<Todo>(_todoValidator.ErrorMessage);
        
        try
        {
            _todoRepository.Update(updatedTodo);
            return _responseCreator.CreateOk(updatedTodo);
        }
        catch (Exception e)
        {
            return _responseCreator.CreateError<Todo>(e.Message);
        }
    }
    
    public ServiceResponse<int> Delete(int id)
    {
        if (!_todoRepository.IsIdExists(id))
            return _responseCreator.CreateError<int>($"Todo {id} not found");
        
        try
        {
            _todoRepository.Delete(id);
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
            _todoRepository.DeleteAll();
            return _responseCreator.CreateOk(string.Empty);
        }
        catch (Exception e)
        {
            return _responseCreator.CreateError<string>(e.Message);
        }
    }
}