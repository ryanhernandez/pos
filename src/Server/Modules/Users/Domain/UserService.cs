using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Modules.Users.Domain
{
    public class UserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> CreateUserAsync(string username, string email, string? displayName = null)
        {
            var existing = await _repository.GetByUsernameAsync(username);
            if (existing != null) throw new InvalidOperationException("Username already exists");

            var user = new User(username, email, displayName);
            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> ListUsersAsync()
        {
            return await _repository.ListAllAsync();
        }

        public async Task<User?> GetUserAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<User?> UpdateUserAsync(Guid id, string username, string email, string? displayName = null)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return null;

            user.Update(username, email, displayName);
            await _repository.UpdateAsync(user);
            await _repository.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
