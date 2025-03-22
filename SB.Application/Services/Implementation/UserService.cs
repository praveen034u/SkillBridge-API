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
    public class EmployeeUserService : IUserService<EmployeeUser>
    {
        private readonly IUserRepository<EmployeeUser> _userRepository;

        public EmployeeUserService(IUserRepository<EmployeeUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task DeleteUserAsync(string id)
        {
            await _userRepository.DeleteAsync(id);
        }

        Task<IEnumerable<EmployeeUser>> IUserService<EmployeeUser>.GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new EmployeeUser
            {
                Id = u.id,
                FirstName = u.firstName,
                LastName = u.lastName,
                Email = u.email,
                Role = u.role.ToString(),
                IsActive = u.isActive
            });
        }

        Task<EmployeeUser> IUserService<EmployeeUser>.GetUserByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user != null ? new UserDto
            {
                Id = user.id,
                FirstName = user.firstName,
                LastName = user.lastName,
                Email = user.email,
                Role = user.role.ToString(),
                IsActive = user.isActive
            } : null;
        }

        public Task AddUserAsync(EmployeeUser userDto)
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

        public Task UpdateUserAsync(EmployeeUser userDto)
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
    }

}
