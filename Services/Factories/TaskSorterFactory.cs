using TheMostBoringNotesApp.Services.Enums;
using Task = TheMostBoringNotesApp.Models.Task;

namespace TheMostBoringNotesApp.Services.Factories;

public static class TaskSorterFactory
{
    public static Func<List<Task>, List<Task>> GetSorter(TaskSortType sortType, int sortOrder)
    {
        var ascending = sortOrder == 1;

        return sortType switch
        {
            TaskSortType.Content => tasks => ascending ? tasks.OrderBy(n => n.Content).ToList() : tasks.OrderByDescending(n => n.Content).ToList(),
            TaskSortType.CreatedAt => tasks => ascending ? tasks.OrderBy(n => n.CreatedAt).ToList() : tasks.OrderByDescending(n => n.CreatedAt).ToList(),
            TaskSortType.UpdatedAt => tasks => ascending ? tasks.OrderBy(n => n.UpdatedAt).ToList() : tasks.OrderByDescending(n => n.UpdatedAt).ToList(),
            _ => throw new ArgumentException($"Unknown sort type: {sortType}")
        };
    }
}