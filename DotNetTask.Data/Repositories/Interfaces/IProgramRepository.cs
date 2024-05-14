using DotNetTask.Data.Entities;

namespace DotNetTask.Data.Repositories.Interfaces
{
    public interface IProgramRepository
    {
        Task<ProgramData> AddProgramAsync(ProgramData program);
        Task<ProgramData> UpdateProgramAsync(string id, ProgramData program);
        Task<ProgramData> GetProgramQuestionsByIdAsync(string id);
    }
}
