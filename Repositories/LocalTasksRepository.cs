using TheMostBoringNotesApp.Repositories.Interfaces;
using Task = TheMostBoringNotesApp.Models.Task;

namespace TheMostBoringNotesApp.Repositories;

public class LocalTasksRepository : LocalGenericRepository<Task>, ITasksRepository
{
    public LocalTasksRepository(
        string localStoragePath, 
        string loggerName, 
        string repositoryObjectName) 
        : base(localStoragePath, loggerName, repositoryObjectName) { }
}