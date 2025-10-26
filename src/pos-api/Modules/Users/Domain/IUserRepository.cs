using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Modules.Users.Domain
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByUsernameAsync(string username);
        Task<IEnumerable<User>> ListAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
        Task<int> SaveChangesAsync();
    }
}
