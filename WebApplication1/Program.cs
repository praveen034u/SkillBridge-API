using Azure.AI.DocumentIntelligence;
using Azure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SB.Application.Features.Users.Commands;
using SB.Application.Services.Implementation;
using SB.Application.Services.Interface;
using SB.Infrastructure;
using SB.Infrastructure.Persistence;
using SB.Infrastructure.Repositories.Implementation;
using SB.Infrastructure.Repositories.Interfaces;
using System.Configuration;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using SB.Application;
using SB.Domain;
using SB.Application.Queries;
using SB.Infrastructure.Services;
using SB.Application.Commands;
using SB.Application.Features.Profile.Commands;


var builder = WebApplication.CreateBuilder(args);
// Access the configuration
IConfiguration configuration = builder.Configuration;

// Add Cosmos DB Client
builder.Services.AddSingleton<CosmosClient>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var endpoint = configuration["CosmosDb:AccountEndpoint"];
    var key = configuration["CosmosDb:AccountKey"];
    return new CosmosClient(endpoint, key);
});

// Add Application Services
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<IProductService, ProductService>();


//builder.Services.AddSingleton<CosmosClient>(sp =>
//{
//    var configuration = sp.GetRequiredService<IConfiguration>();
//    var endpoint = configuration["CosmosDb:AccountEndpoint"];
//    var key =  configuration["CosmosDb:AccountKey"];
//    return new CosmosClient(endpoint, key);
//});

builder.Services.AddScoped<UserRepository>();


builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddMediatR(config =>
{
    // Register handlers in the current assembly
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.RegisterServicesFromAssembly(typeof(RegisterUserCommandHandler).Assembly); // Specify the handler assembly
    config.RegisterServicesFromAssembly(typeof(UploadResumeCommandHandler).Assembly); // Specify the handler assembly
    config.RegisterServicesFromAssembly(typeof(SearchJobsQueryHandler).Assembly); // Specify the handler assembly
    config.RegisterServicesFromAssembly(typeof(CreateJobPostingHandler).Assembly); // Specify the handler assembly
    config.RegisterServicesFromAssembly(typeof(SearchJobsBySkillsQueryHandler).Assembly);
});

builder.Services.AddSingleton<CosmosDbContext>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var endpoint = configuration["CosmosDb1:ConnectionString"];
    var databaseName = configuration["CosmosDb1:DatabaseName"];

    if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(databaseName))
    {
        throw new ArgumentNullException("Cosmos DB configuration is missing.");
    }

    return new CosmosDbContext(endpoint, databaseName);
});

builder.Services.AddSingleton<ProfileDbContext>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var endpoint = configuration["CosmosDb1:ConnectionString"];
    var databaseName = configuration["CosmosDb1:DatabaseName"];

    if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(databaseName))
    {
        throw new ArgumentNullException("Cosmos DB configuration is missing.");
    }

    return new ProfileDbContext(endpoint, databaseName);
});

// Add Controllers
builder.Services.AddControllers();
//builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.MapType<IFormFile>(() => new Microsoft.OpenApi.Models.OpenApiSchema
    {
        Type = "string",
        Format = "binary"
    });
});
builder.Services.AddSingleton<DocumentIntelligenceClient>(sp =>
{
    string endpoint = builder.Configuration["DocumentIntelligence:Endpoint"];
    string apiKey = builder.Configuration["DocumentIntelligence:ApiKey"];

    var credential = new AzureKeyCredential(apiKey);
    var client = new DocumentIntelligenceClient(new Uri(endpoint), credential);
    return client;
});

// Add configuration for AzureOpenAISettings
builder.Services.Configure<SB.Application.Features.Profile.Commands.OpenAI>(builder.Configuration.GetSection("OpenAI"));

// Register repository
builder.Services.AddScoped<IJobPostingRepository, JobPostingRepository>();
// Register IHttpClientFactory
builder.Services.AddHttpClient();

// Register UploadResumeCommandHandler
//builder.Services.AddScoped<SB.Application.Features.Profile.Commands.UploadResumeCommandHandler>();

string blobConnectionString = builder.Configuration["ConnectionStringsBlob:AzureBlobStorage"]; // configuration.GetConnectionString("ConnectionStringsBlob:AzureBlobStorage");
// Load settings from configuration
builder.Services.Configure<AzureCognitiveSearch>(configuration.GetSection("AzureCognitiveSearch"));
// Register BlobServiceClient with DI
builder.Services.AddSingleton(new BlobServiceClient(blobConnectionString));



//builder.Services.Configure<AzureSearchSettings>(
//    builder.Configuration.GetSection("AzureSearch"));

// Register MediatR (Ensure Application assembly is included)
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SearchJobsQueryHandler).Assembly));
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateJobPostingHandler).Assembly));

// Register Azure AI Search dependencies
builder.Services.AddSingleton<JobSearchIndexService>();
builder.Services.AddSingleton<JobSearchService>();

builder.Services.AddSingleton<IJobSearchRepository, JobSearchRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArchitecture API v1"));
}

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
//})
//.AddCookie()
//.AddGoogle(options =>
//{
//    options.ClientId = "Your-Client-ID";
//    options.ClientSecret = "Your-Client-Secret";
//});


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
