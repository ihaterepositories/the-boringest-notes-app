using TodoAPI.Models;
using TodoAPI.Services.Enums;

namespace TodoAPI.Services.Factories;

public class SortFactory
{
    public static Func<List<Todo>, List<Todo>> GetSorter(SortType sortType, int sortOrder)
    {
        var ascending = sortOrder == 1;

        return sortType switch
        {
            SortType.Content => todos => ascending ? todos.OrderBy(n => n.Content).ToList() : todos.OrderByDescending(n => n.Content).ToList(),
            SortType.CreatedAt => todos => ascending ? todos.OrderBy(n => n.CreatedAt).ToList() : todos.OrderByDescending(n => n.CreatedAt).ToList(),
            SortType.UpdatedAt => todos => ascending ? todos.OrderBy(n => n.UpdatedAt).ToList() : todos.OrderByDescending(n => n.UpdatedAt).ToList(),
            _ => throw new ArgumentException($"Unknown sort type: {sortType}")
        };
    }
}