using TheMostBoringNotesApp.Services.Enums;
using Task = TheMostBoringNotesApp.Models.Task;

namespace TheMostBoringNotesApp.Services.Factories;

public static class TasksSorterFactory
{
    public static Func<List<Task>, List<Task>> GetSorter(TasksSortType sortType, int sortOrder)
    {
        var ascending = sortOrder == 1;

        return sortType switch
        {
            TasksSortType.Status => tasks => ascending ? tasks.OrderBy(n => n.IsDone).ToList() : tasks.OrderByDescending(n => n.IsDone).ToList(),
            TasksSortType.CreatedAt => tasks => ascending ? tasks.OrderBy(n => n.CreatedAt).ToList() : tasks.OrderByDescending(n => n.CreatedAt).ToList(),
            TasksSortType.UpdatedAt => tasks => ascending ? tasks.OrderBy(n => n.UpdatedAt).ToList() : tasks.OrderByDescending(n => n.UpdatedAt).ToList(),
            _ => throw new ArgumentException($"Unknown sort type: {sortType}")
        };
    }
}