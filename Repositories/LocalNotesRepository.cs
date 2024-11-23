using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TheMostBoringNotesApp.Models;
using TheMostBoringNotesApp.Repositories.Interfaces;
using TheMostBoringNotesApp.Utils;
using TheMostBoringNotesApp.ViewCore;

namespace TheMostBoringNotesApp.Repositories;

public class LocalNotesRepository : INotesRepository
{
    private readonly string? _filePath;
    private readonly Logger _logger = new Logger("LocalNotesRepository");
    
    private readonly List<Note> _notes;
    
    public LocalNotesRepository(IConfiguration configuration)
    {
        _filePath = configuration["NotesRepository:FilePath"];
        _notes = LoadNotesFromFile();
        
        if (_notes.Count == 0)
        {
            _notes.Add(new Note(TextSnippets.WelcomeNoteTitle, TextSnippets.WelcomeNoteContent));
            SaveNotesToFile();
        }
    }
    
    public List<Note> GetAll()
    {
        if (_notes.Count == 0)
            _logger.LogWarning("No notes found");
        
        return _notes;
    }

    public Note GetById(Guid id)
    {
        var note = _notes.FirstOrDefault(n => n.Id == id);
        
        if (note == null)
        {
            _logger.LogWarning($"Note with id {id} not found");
            return new Note(string.Empty, string.Empty);
        }
        
        return note;
    }

    public void Add(Note note)
    {
        _notes.Add(note);
        SaveNotesToFile();
    }

    public void Update(Note note)
    {
        var existingNote = _notes.FirstOrDefault(n => n.Id == note.Id);
        
        if (existingNote == null)
        {
            _logger.LogWarning($"Can`t update note with id {note.Id}, note not found");
            return;
        }
        
        existingNote.Update(note.Content);
        
        SaveNotesToFile();
    }

    public void Delete(Guid id)
    {
        var note = _notes.FirstOrDefault(n => n.Id == id);
        
        if (note == null)
        {
            _logger.LogWarning($"Can`t delete note with id {id}, note not found");
            return;
        }
        
        _notes.Remove(note);
        SaveNotesToFile();
    }
    
    private List<Note> LoadNotesFromFile()
    {
        if (!File.Exists(_filePath))
        {
            _logger.LogWarning($"Can`t load notes, file not found at {_filePath}");
            return [];
        }

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Note>>(json) ?? [];
    }

    private void SaveNotesToFile()
    {
        try
        {
            var json = JsonSerializer.Serialize(_notes);
            
            if (_filePath != null)
                File.WriteAllText(_filePath, json);
            else
                _logger.LogError("Can`t save notes, file path is not set");
        }
        catch (Exception e)
        {
            _logger.LogError($"Can`t save notes to file: {e.Message}");
        }
    }
}