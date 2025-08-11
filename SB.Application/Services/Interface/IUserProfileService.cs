using SB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Interface
{
    public interface IUserProfileService
    {
        Task<IEnumerable<UserProfile>> GetAllAsync();
        Task<UserProfile> GetByIdAsync(int id);
        Task<UserProfile> GetUserByEmailAsync(string email);
        Task<UserProfile> CreateAsync(UserProfile user);
        Task<bool> UpdateAsync(UserProfile user);
        Task<bool> DeleteAsync(int id);
    }
}
