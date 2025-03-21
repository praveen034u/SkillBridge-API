//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using SB.Infrastructure.Persistence;
//using System;
//using System.Runtime.Intrinsics.X86;

//// Infrastructure Layer - Dependency Injection for CosmosDB

//namespace SB.Infrastructure;

//public static class DependencyInjection
//{
//    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
//    {
//        var connectionString = configuration["CosmosDb:ConnectionString"];
//        var databaseName = configuration["CosmosDb:DatabaseName"];

//        services.AddSingleton(new CosmosDbContext(connectionString, databaseName));
//        services.AddSingleton(SB.Application.Services.Interface.ius IUserService, userservi));

//        return services;
//    }
//}

