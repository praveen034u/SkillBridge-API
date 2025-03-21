using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Infrastructure.Repositories.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<SB.Domain.Entities.User> GetUserProfileByIdAsync(string userId);
    }
}
