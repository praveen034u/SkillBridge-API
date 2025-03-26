using SB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task AddAsync(T user);
        Task UpdateAsync(T user);
        Task DeleteAsync(string id);
    }

}
