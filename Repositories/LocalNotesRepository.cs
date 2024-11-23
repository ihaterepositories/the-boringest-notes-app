using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TheMostBoringNotesApp.Models;
using TheMostBoringNotesApp.Repositories.Interfaces;
using TheMostBoringNotesApp.Utils;
using TheMostBoringNotesApp.ViewCore;

namespace TheMostBoringNotesApp.Repositories;

public class LocalNotesRepository : LocalGenericRepository<Note>, INotesRepository
{
    public LocalNotesRepository(
        string localStoragePath, 
        string loggerName, 
        string repositoryObjectName) 
        : base(localStoragePath, loggerName, repositoryObjectName) { }
}