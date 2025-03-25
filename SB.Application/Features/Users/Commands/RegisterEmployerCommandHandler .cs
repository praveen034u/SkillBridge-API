using MediatR;
using Microsoft.Azure.Cosmos;
using SB.Infrastructure.Persistence;
using System.Text.Json;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Hosting;

// Application Layer - Register User Command
namespace SB.Application.Features.Users.Commands;

public class RegisterEmployerCommand :  IRequest<string>
{
    public EmployerUser UserProfile { get; set; }
    
    //public string FirstName { get; set; }
    //public string LastName { get; set; }
    //public string Email { get; set; }
    //public string Address { get; set; }
    //public string State { get; set; }
    //public string City { get; set; }
    //public string Role { get; set; }
    //public List<string> Skills { get; set; }
    //public string PhoneNumber { get; set; }
    //public string Country { get; set; }
}

public class RegisterEmployerCommandHandler : IRequestHandler<RegisterEmployerCommand, string>
{
    private readonly CosmosDbContext _dbContext;
    private Mapper mapper;

    public RegisterEmployerCommandHandler(CosmosDbContext dbContext)
    {
        _dbContext = dbContext;
        mapper = new Mapper();
    }

    public async Task<string> Handle(RegisterEmployerCommand request, CancellationToken cancellationToken)
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
        var userInfo = new SB.Domain.Entities.UserInfo
        {
            Address = request.UserProfile.Address,
            name = request.UserProfile.Name,
            Email = request.UserProfile.Email,
            Phone = request.UserProfile.PhoneNumber
        };


        // Create new user
        var user = new Domain.Entities.EmployerUser
        {
            id= Guid.NewGuid().ToString(),
            categoryId= Guid.NewGuid().ToString(),
            isActive=true,
            userProfile= JsonSerializer.Serialize(userInfo) ,
            Role="Employer",
            CompanyLocations= mapper.Map<List<Domain.Entities.CompanyLocation>>(request.UserProfile?.CompanyLocations),
            CompanyName= request.UserProfile.CompanyName,
            CompanyWebsiteUrl= request.UserProfile.CompanyWebsiteUrl

            //request.Role.ToString(), hadcoded later will combine with employer
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

