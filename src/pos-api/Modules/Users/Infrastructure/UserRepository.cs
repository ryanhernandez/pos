using Microsoft.EntityFrameworkCore;
using Server.Shared.Infrastructure;
using Server.Modules.Users.Domain;

namespace Server.Modules.Users.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(User user)
        {
            await _db.Set<User>().AddAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _db.Set<User>().FindAsync(id);
            if (user != null)
                _db.Set<User>().Remove(user);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _db.Set<User>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _db.Set<User>().AsNoTracking().FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<IEnumerable<User>> ListAllAsync()
        {
            return await _db.Set<User>().AsNoTracking().ToListAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }

        public Task UpdateAsync(User user)
        {
            _db.Set<User>().Update(user);
            return Task.CompletedTask;
        }
    }
}
