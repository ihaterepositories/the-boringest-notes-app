using TheMostBoringNotesApp.Models;

namespace TheMostBoringNotesApp.Repositories.Interfaces;

public interface INotesRepository
{
    public List<Note> GetAll();
    public Note GetById(Guid id);
    public void Add(Note note);
    public void Update(Note note);
    public void Delete(Guid id);
}