using DotNetTask.API.CosmosSetUp;
using DotNetTask.API.Services.Implementations;
using DotNetTask.API.Services.Interfaces;
using DotNetTask.Data.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var _configuration = builder.Configuration.GetSection("CosmosDb");
var _initializeCosmos = new CosmosInitializer(_configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddSingleton<IProgramRepository>(_initializeCosmos.InitializeProgramRepositoryInstanceAsync().GetAwaiter().GetResult());
builder.Services.AddSingleton<IApplicationRepository>(_initializeCosmos.InitializeApplicationRepositoryInstanceAsync().GetAwaiter().GetResult());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
