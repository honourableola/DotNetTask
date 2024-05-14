using DotNetTask.Data.Entities;
using DotNetTask.Data.Repositories.Interfaces;
using Microsoft.Azure.Cosmos;

namespace DotNetTask.Data.Repositories.Implementations
{
    public class ProgramRepository : IProgramRepository
    {
        private readonly Container _container;

        public ProgramRepository(CosmosClient cosmosDbClient, string databaseName, string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }
        public async Task<ProgramData> AddProgramAsync(ProgramData program)
        {
            var response = await _container.CreateItemAsync(program, new PartitionKey(program.Id));
            return response.Resource;
        }

        public async Task<ProgramData> GetProgramQuestionsByIdAsync(string id)
        {
            var response = await _container.ReadItemAsync<ProgramData>(id, new PartitionKey(id));
            return response.Resource;
        }

        public async Task<ProgramData> UpdateProgramAsync(string id, ProgramData program)
        {
            var response = await _container.UpsertItemAsync(program, new PartitionKey(id));
            return response.Resource;
        }

        public async Task<ProgramData> GetProgramByTitleAsync(string programTitle)
        {
            var query = new QueryDefinition("SELECT * FROM Programs p WHERE p.ProgramTitle = @ProgramTitle")
                                            .WithParameter("@ProgramTitle", programTitle);

            var iterator = _container.GetItemQueryIterator<ProgramData>(query);
            var response = await iterator.ReadNextAsync();

            return response.FirstOrDefault();
        }
    }
}
