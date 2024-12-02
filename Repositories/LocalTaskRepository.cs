using TheMostBoringNotesApp.Repositories.Interfaces;
using TheMostBoringNotesApp.Utils.Notifiers.Interfaces;
using Task = TheMostBoringNotesApp.Models.Task;

namespace TheMostBoringNotesApp.Repositories;

public class LocalTaskRepository : LocalGenericRepository<Task>, ITaskRepository
{
    public LocalTaskRepository(
        string localStoragePath, 
        INotificator notificator, 
        string repositoryObjectName) 
        : base(localStoragePath, notificator, repositoryObjectName) { }
}