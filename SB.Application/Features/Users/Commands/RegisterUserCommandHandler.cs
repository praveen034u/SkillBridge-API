using MediatR;
using Microsoft.Azure.Cosmos;
using SB.Domain.Entities;
using SB.Domain.ValueObjects;
using SB.Infrastructure.Persistence;
using System.ComponentModel;
using System.Text.Json;


// Application Layer - Register User Command


namespace SB.Application.Features.Users.Commands;

public class RegisterUserCommand :  IRequest<string>
{
    public EmployeeUser UserProfile { get; set; }
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
{
    private readonly CosmosDbContext _dbContext;

    public RegisterUserCommandHandler(CosmosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Ensure the container exists before inserting data
        var container = _dbContext.GetContainer();

       // This checks if the container exists
        // Check if user already exists
        var existingUser = await _dbContext.GetUserByEmailAsync(request.UserProfile.Email);
        if (existingUser != null)
        {
            throw new Exception("User already exists with this email.");
        }
  
        // Create new user
        var user = new Domain.Entities.User
        {
            id= Guid.NewGuid().ToString(),
            categoryId= Guid.NewGuid().ToString(),
            isActive=true,
            userProfile= JsonSerializer.Serialize(new { 
                name = request.UserProfile.Name, 
                email = request.UserProfile.Email,
                address= request.UserProfile,
                phone=request.UserProfile.PhoneNumber}) ,
            role = "Employee" //request.Role.ToString(), hadcoded later will combine with employer
            //skills = request.Skills.Select(skill => new Skill(skill, 1)).ToList()
        };
        try
        {
            // Verify container exists before adding
            await container.ReadContainerAsync();

            await container.CreateItemAsync(user, new PartitionKey(user.categoryId));
            await _dbContext.AddUserAsync(user);
        }
        catch (CosmosException ex) 
        {
            Console.WriteLine($"Error: {ex.StatusCode}, SubStatus: {ex.SubStatusCode}, Message: {ex.Message}");
        }
        catch (Exception ex)
        {

            throw;
        }
     
        return user.id.ToString();
    }
}

