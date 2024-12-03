using TheMostBoringNotesApp.Repositories;
using TheMostBoringNotesApp.Repositories.Interfaces;
using TheMostBoringNotesApp.Services;
using TheMostBoringNotesApp.Utils.Notifiers;
using TheMostBoringNotesApp.Utils.Notifiers.Interfaces;
using TheMostBoringNotesApp.Utils.Validators;
using TheMostBoringNotesApp.View.Interfaces;
using TheMostBoringNotesApp.View.ViewCores.Console;
using TheMostBoringNotesApp.View.ViewCores.Console.CommandFactories;

string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
string appFolder = Path.Combine(appDataPath, "TheMostBoringTodoApp");
if (!Directory.Exists(appFolder))
    Directory.CreateDirectory(appFolder);
if (!File.Exists(Path.Combine(appFolder, "tasks.json")))
    File.WriteAllText(Path.Combine(appFolder, "tasks.json"), "[]");

INotificator consoleNotificator = new ConsoleNotificator();
var tasksValidator = new TaskValidator(consoleNotificator);
ITaskRepository taskRepository = new LocalTaskRepository(
    appFolder + "/tasks.json", 
    consoleNotificator, 
    "task"
);
var tasksService = new TaskService(taskRepository, tasksValidator, consoleNotificator);
var viewCore = new ConsoleViewCore(tasksService, consoleNotificator);

viewCore.Run();