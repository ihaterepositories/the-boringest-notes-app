using TodoAPI.Models;
using TodoAPI.Services.Enums;

namespace TodoAPI.Services.Factories;

public class GetByDateFactory
{
    public static Func<List<Todo>, List<Todo>> GetByDate(GetByDateOption dateOption)
    {
        return dateOption switch
        {
            GetByDateOption.Today => todos => todos.Where(n => n.CreatedAt.Date == DateTime.Today).ToList(),
            GetByDateOption.Yesterday => todos => todos.Where(n => n.CreatedAt.Date == DateTime.Today.AddDays(-1)).ToList(),
            GetByDateOption.ThisWeek => todos => todos.Where(n => n.CreatedAt.Date >= DateTime.Today.AddDays(-((int)DateTime.Today.DayOfWeek - 1))).ToList(),
            GetByDateOption.LastWeek => todos => todos.Where(n => n.CreatedAt.Date < DateTime.Today.AddDays(-((int)DateTime.Today.DayOfWeek - 1)) && n.CreatedAt.Date >= DateTime.Today.AddDays(-((int)DateTime.Today.DayOfWeek + 6))).ToList(),
            GetByDateOption.ThisMonth => todos => todos.Where(n => n.CreatedAt.Month == DateTime.Today.Month).ToList(),
            GetByDateOption.LastMonth => todos => todos.Where(n => n.CreatedAt.Month == DateTime.Today.AddMonths(-1).Month).ToList(),
            GetByDateOption.ThisYear => todos => todos.Where(n => n.CreatedAt.Year == DateTime.Today.Year).ToList(),
            GetByDateOption.LastYear => todos => todos.Where(n => n.CreatedAt.Year == DateTime.Today.AddYears(-1).Year).ToList(),
            _ => throw new ArgumentException($"Unknown date option: {dateOption}")
        };
    }
}