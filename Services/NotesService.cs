using TheMostBoringNotesApp.Models;
using TheMostBoringNotesApp.Repositories.Interfaces;
using TheMostBoringNotesApp.Services.Enums;
using TheMostBoringNotesApp.Services.Factories;
using TheMostBoringNotesApp.Utils;
using TheMostBoringNotesApp.Utils.Validators;

namespace TheMostBoringNotesApp.Services;

public class NotesService(INotesRepository notesRepository)
{
    private readonly Logger _logger = new Logger("NotesService");
    private readonly NoteValidator _noteValidator = new NoteValidator();
    
    public List<Note> GetToday()
    {
        var notes = notesRepository.GetAll();
        return notes.Where(n => n.CreatedAt.Date == DateTime.Today).ToList();
    }
    
    public List<Note> GetAll(NotesSortType sortType, int sortOrder, int limit = 0)
    {
        var notes = notesRepository.GetAll();
        var sorter = NotesSorterFactory.GetSorter(sortType, 1);
        return limit == 0 ? sorter(notes) : sorter(notes).Take(limit).ToList();
    }
    
    public Note GetById(Guid id)
    {
        return notesRepository.GetById(id);
    }
    
    public void Add(Note note)
    {
        if (!_noteValidator.Validate(note)) return;
        notesRepository.Add(note);
    }
    
    public void Update(Note note)
    {
        if (!_noteValidator.Validate(note)) return;
        notesRepository.Update(note);
    }
    
    public void Delete(Guid id)
    {
        notesRepository.Delete(id);
    }
}