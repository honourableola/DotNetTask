using DotNetTask.Data.Models;

namespace DotNetTask.API.Services.Interfaces
{
    public interface IProgramService
    {
        Task<BaseResponse> AddProgramAsync(CreateProgramRequest request);
        Task<BaseResponse> EditProgramAsync(string id, UpdateProgramRequest request);
        Task<ProgramResponse> GetProgramQuestionsByIdAsync(string id);
    }
}
