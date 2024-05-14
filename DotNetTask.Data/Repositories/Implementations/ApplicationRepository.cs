using DotNetTask.Data.Entities;
using DotNetTask.Data.Repositories.Interfaces;
using Microsoft.Azure.Cosmos;

namespace DotNetTask.Data.Repositories.Implementations
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly Container _container;

        public ApplicationRepository(CosmosClient cosmosDbClient, string databaseName, string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }
        public async Task<Application> AddApplicationAsync(Application application)
        {
            var response = await _container.CreateItemAsync(application, new PartitionKey(application.Id));
            return response.Resource;
        }

        public async Task<Application> GetApplicationWithEmailAsync(string email)
        {
            var query = new QueryDefinition("SELECT * FROM Applications a WHERE a.Email = @Email")
                                            .WithParameter("@Email", email);

            var iterator = _container.GetItemQueryIterator<Application>(query);
            var response = await iterator.ReadNextAsync();

            return response.FirstOrDefault();
        }
    }
}
