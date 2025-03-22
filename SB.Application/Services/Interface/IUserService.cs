using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Interface
{
    public interface IUserService<T>
    {
        Task<IEnumerable<T>> GetAllUsersAsync();
        Task<T> GetUserByIdAsync(string id);
        Task AddUserAsync(T userDto);
        Task UpdateUserAsync(T userDto);
        Task DeleteUserAsync(string id);
    }
}
