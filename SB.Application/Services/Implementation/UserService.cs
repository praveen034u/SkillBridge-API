using SB.Application.Services.Interface;
using SB.Domain.Entities;
using SB.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDto
            {
                Id = u.id,
                FirstName = u.firstName,
                LastName = u.lastName,
                Email = u.email,
                Role = u.role.ToString(),
                IsActive = u.isActive
            });
        }

        public async Task<UserDto?> GetUserByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user != null ? new UserDto
            {
                Id =  user.id,
                FirstName = user.firstName,
                LastName = user.lastName,
                Email = user.email,
                Role = user.role.ToString(),
                IsActive = user.isActive
            } : null;
        }

        public async Task AddUserAsync(UserDto userDto)
        {
            var user = new User
            {
                id = userDto.Id,
                firstName = userDto.FirstName,
                lastName = userDto.LastName,
                email = userDto.Email,
                role = userDto.Role,
                isActive = userDto.IsActive
            };
            await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            var user = new User
            {
                id = userDto.Id,
                firstName = userDto.FirstName,
                lastName = userDto.LastName,
                email = userDto.Email,
                role = userDto.Role,
                isActive = userDto.IsActive
            };
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(string id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }

}
