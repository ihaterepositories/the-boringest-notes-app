using TheMostBoringNotesApp.Repositories;
using TheMostBoringNotesApp.Repositories.Interfaces;
using TheMostBoringNotesApp.Services;

INotesRepository notesRepository = new LocalNotesRepository(
    "LocalStorage/notes.json", 
    "NotesRepository", 
    "note"
);

ITasksRepository tasksRepository = new LocalTasksRepository(
    "LocalStorage/tasks.json", 
    "TasksRepository", 
    "task"
);

NotesService notesService = new NotesService(notesRepository);