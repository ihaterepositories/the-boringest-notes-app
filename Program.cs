using TheMostBoringNotesApp.Repositories;
using TheMostBoringNotesApp.Repositories.Interfaces;
using TheMostBoringNotesApp.Services;
using TheMostBoringNotesApp.Utils.Validators;
using TheMostBoringNotesApp.View.Interfaces;
using TheMostBoringNotesApp.View.Notifiers;
using TheMostBoringNotesApp.View.Notifiers.Interfaces;
using TheMostBoringNotesApp.View.ViewCores.TextInterfaces;

string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
string appFolder = Path.Combine(appDataPath, "TheMostBoringTodoApp");
if (!Directory.Exists(appFolder))
    Directory.CreateDirectory(appFolder);
if (!File.Exists(Path.Combine(appFolder, "tasks.json")))
    File.WriteAllText(Path.Combine(appFolder, "tasks.json"), "[]");

INotificator consoleNotificator = new ConsoleNotificator();
ITaskRepository taskRepository = new LocalTaskRepository(
    appFolder + "/tasks.json", 
    consoleNotificator, 
    "task"
);
var tasksService = new TaskService(taskRepository);
var viewCore = new ConsoleViewCore(tasksService, consoleNotificator);

viewCore.Run();