using TheMostBoringNotesApp.Models;

namespace TheMostBoringNotesApp.Utils.Validators;

public class NoteValidator
{
    private readonly Logger _logger = new Logger("NoteValidator");
    
    public bool Validate(Note note)
    {
        if (string.IsNullOrWhiteSpace(note.Content))
        {
            _logger.LogError("Note validation failed, content is empty");
            return false;
        }
        
        return true;
    }
}