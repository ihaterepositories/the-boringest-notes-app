using TheMostBoringNotesApp.Models;
using TheMostBoringNotesApp.Services.Enums;

namespace TheMostBoringNotesApp.Services.Factories;

public static class NotesSorterFactory
{
    public static Func<List<Note>, List<Note>> GetSorter(NotesSortType sortType, int sortOrder)
    {
        var ascending = sortOrder == 1;

        return sortType switch
        {
            NotesSortType.CreatedAt => notes => ascending ? notes.OrderBy(n => n.CreatedAt).ToList() : notes.OrderByDescending(n => n.CreatedAt).ToList(),
            NotesSortType.UpdatedAt => notes => ascending ? notes.OrderBy(n => n.UpdatedAt).ToList() : notes.OrderByDescending(n => n.UpdatedAt).ToList(),
            _ => throw new ArgumentException($"Unknown sort type: {sortType}")
        };
    }
}