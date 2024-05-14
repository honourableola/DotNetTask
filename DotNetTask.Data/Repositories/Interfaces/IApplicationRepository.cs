using DotNetTask.Data.Entities;

namespace DotNetTask.Data.Repositories.Interfaces
{
    public interface IApplicationRepository
    {
        Task<Application> AddApplicationAsync(Application application);
        Task<Application> GetApplicationWithEmailAsync(string email);
    }
}
