using MediatR;
using SB.Domain.ValueObjects;
using SB.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SB.Domain.Entities;


// Application Layer - Register User Command


namespace SB.Application.Features.Users.Commands;

public class RegisterUserCommand :  IRequest<string>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public List<string> Skills { get; set; }
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
        // Check if user already exists
        var existingUser = await _dbContext.GetUserByEmailAsync(request.Email);
        if (existingUser != null)
        {
            throw new Exception("User already exists with this email.");
        }

        // Create new user
        var user = new User
        {
            id= Guid.NewGuid().ToString(),
            categoryId= Guid.NewGuid().ToString(),
            firstName = request.FirstName,
            lastName = request.LastName,
            email = request.Email,
            role = request.Role.ToString(),
            skills = request.Skills.Select(skill => new Skill(skill, 1)).ToList()
        };
        try
        {
            await _dbContext.AddUserAsync(user);
        }
        catch (Exception ex)
        {

            throw;
        }

     
        return user.id.ToString();
    }
}

