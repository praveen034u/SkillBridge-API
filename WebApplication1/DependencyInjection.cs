using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SB.Application;
using SB.Application.Services.Implementation;
using SB.Application.Services.Interface;
using SB.Domain;
using SB.Infrastructure.Persistence;
using SB.Infrastructure.Repositories.Implementation;
using SB.Infrastructure.Repositories.Interfaces;
using System;

// Infrastructure Layer - Dependency Injection for CosmosDB

namespace SB.API;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["CosmosDb1:ConnectionString"];
        var databaseName = configuration["CosmosDb1:DatabaseName"];

        services.AddSingleton(new CosmosDbContext(connectionString, databaseName));
        services.AddSingleton(new ProfileDbContext(connectionString, databaseName));
        services.AddSingleton<SB.Application.Services.Interface.IUserService, SB.Application.Services.Implementation.UserService>();
        services.AddSingleton<IJobPostingRepository, JobPostingRepository>();
        services.AddSingleton<SB.Infrastructure.Repositories.Interfaces.IUserRepository, SB.Infrastructure.Repositories.Implementation.UserRepository>();
        //services.Configure<AzureCognitiveSearch>(configuration.GetSection("AzureSearch"));
       //services.AddSingleton<IJobSearchRepository, JobSearchRepository>();

        // services.AddTransient<IJobSearchService, AzureSearchService>();
        return services;
    }
}

