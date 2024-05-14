using DotNetTask.Data.Models;

namespace DotNetTask.API.Services.Interfaces
{
    public interface IApplicationService
    {
        Task<BaseResponse> AddApplicationAsync(string programId, ApplicationRequest application);
    }
}
