using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using SB.Application.Services.Implementation;
using SB.Application.Services.Interface;
using SB.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Features.Users.Commands;

public class RegisterUserProfileCommand : IRequest<string>
{
    public UserProfile UserProfile { get; set; }


}
public class RegisterUserProfileCommandHandler : IRequestHandler<RegisterUserProfileCommand, string>
{
    private readonly IUserProfileService _userProfileService;

    public RegisterUserProfileCommandHandler(IUserProfileService userProfileService)
    {
        _userProfileService = userProfileService;
    }

    public async Task<string> Handle(RegisterUserProfileCommand request, CancellationToken cancellationToken)
    {
        UserProfile user = new UserProfile();
        // This checks if the container exists
        // Check if user already exists
        //var existingUser = await _userProfileService.GetUserByEmailAsync(request.UserProfile.Email);
        //if (existingUser != null)
        //{
        //    throw new Exception("User already exists with this email.");
        //}
       // request.UserProfile.Id = 5002;
        try
        {
            user=await _userProfileService.CreateAsync(request.UserProfile);
        }
        catch (CosmosException ex)
        {
            Console.WriteLine($"Error: {ex.StatusCode}, SubStatus: {ex.SubStatusCode}, Message: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw;
        }

        return user.Id.ToString();
    }
}
