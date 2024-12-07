using TodoAPI.Repositories;
using TodoAPI.Repositories.Interfaces;
using TodoAPI.Services;
using TodoView.ViewCores.TextInterfaceCores;
using TodoView.ViewCores.TextInterfaceCores.Notifying;
using TodoView.ViewCores.TextInterfaceCores.Notifying.Interfaces;

string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
string appFolder = Path.Combine(appDataPath, "TheMostBoringTodoApp");

if (!Directory.Exists(appFolder))
    Directory.CreateDirectory(appFolder);
if (!File.Exists(Path.Combine(appFolder, "tasks.json")))
    File.WriteAllText(Path.Combine(appFolder, "tasks.json"), "[]");

INotificator consoleNotificator = new ConsoleNotificator();
ITodoRepository todoRepository = new LocalTodoRepository(appFolder + "/tasks.json", "task");

var todoService = new TodoService(todoRepository);
var viewCore = new ConsoleViewCore(todoService, consoleNotificator);

viewCore.Run();