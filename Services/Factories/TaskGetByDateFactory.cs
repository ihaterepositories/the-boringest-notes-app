using TheMostBoringNotesApp.Services.Enums;
using Task = TheMostBoringNotesApp.Models.Task;

namespace TheMostBoringNotesApp.Services.Factories;

public class TaskGetByDateFactory
{
    public static Func<List<Task>, List<Task>> GetByDate(TaskGetByDateOption dateOption)
    {
        return dateOption switch
        {
            TaskGetByDateOption.Today => tasks => tasks.Where(n => n.CreatedAt.Date == DateTime.Today).ToList(),
            TaskGetByDateOption.Yesterday => tasks => tasks.Where(n => n.CreatedAt.Date == DateTime.Today.AddDays(-1)).ToList(),
            TaskGetByDateOption.ThisWeek => tasks => tasks.Where(n => n.CreatedAt.Date >= DateTime.Today.AddDays(-((int)DateTime.Today.DayOfWeek - 1))).ToList(),
            TaskGetByDateOption.LastWeek => tasks => tasks.Where(n => n.CreatedAt.Date < DateTime.Today.AddDays(-((int)DateTime.Today.DayOfWeek - 1)) && n.CreatedAt.Date >= DateTime.Today.AddDays(-((int)DateTime.Today.DayOfWeek + 6))).ToList(),
            TaskGetByDateOption.ThisMonth => tasks => tasks.Where(n => n.CreatedAt.Month == DateTime.Today.Month).ToList(),
            TaskGetByDateOption.LastMonth => tasks => tasks.Where(n => n.CreatedAt.Month == DateTime.Today.AddMonths(-1).Month).ToList(),
            TaskGetByDateOption.ThisYear => tasks => tasks.Where(n => n.CreatedAt.Year == DateTime.Today.Year).ToList(),
            TaskGetByDateOption.LastYear => tasks => tasks.Where(n => n.CreatedAt.Year == DateTime.Today.AddYears(-1).Year).ToList(),
            _ => tasks => tasks
        };
    }
}