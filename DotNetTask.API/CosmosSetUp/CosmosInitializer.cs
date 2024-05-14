using DotNetTask.Data.Repositories.Implementations;
using Microsoft.Azure.Cosmos;

namespace DotNetTask.API.CosmosSetUp
{
    public class CosmosInitializer
    {
        private readonly IConfiguration _configurationSection;
        private readonly string _account;
        private readonly string _key;
        private readonly string _databaseName;
        private readonly string _applicationContainer;
        private readonly string _programContainer;
        private readonly CosmosClient _client;

        public CosmosInitializer(IConfiguration configurationSection)
        {
            _configurationSection = configurationSection;
            _account = _configurationSection["Account"] ?? throw new ArgumentNullException(nameof(_account)); ;
            _key = configurationSection["Key"] ?? throw new ArgumentNullException(nameof(_key)); ;
            _databaseName = configurationSection["DatabaseName"] ?? throw new ArgumentNullException(nameof(_databaseName)); ;
            _applicationContainer = configurationSection["ApplicationContainer"] ?? throw new ArgumentNullException(nameof(_applicationContainer)); ;
            _programContainer = configurationSection["ProgramContainer"] ?? throw new ArgumentNullException(nameof(_programContainer)); ;
            _client = new CosmosClient(_account, _key);
        }
        public async Task<ProgramRepository> InitializeProgramRepositoryInstanceAsync()
        {
            var database = await _client.CreateDatabaseIfNotExistsAsync(_databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(_programContainer, "/id");
            return new ProgramRepository(_client, _databaseName, _programContainer);
        }

        public async Task<ApplicationRepository> InitializeApplicationRepositoryInstanceAsync()
        {
            var database = await _client.CreateDatabaseIfNotExistsAsync(_databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(_applicationContainer, "/id");
            return new ApplicationRepository(_client, _databaseName, _applicationContainer);
        }
    }
}
