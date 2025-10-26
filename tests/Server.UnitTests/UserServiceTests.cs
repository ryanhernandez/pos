using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Server.Modules.Users.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Server.UnitTests
{
    [TestClass]
    public class UserServiceTests
    {
        class InMemoryUserRepository : IUserRepository
        {
            private readonly List<User> _users = new();

            public Task AddAsync(User user)
            {
                _users.Add(user);
                return Task.CompletedTask;
            }

            public Task DeleteAsync(System.Guid id)
            {
                _users.RemoveAll(u => u.Id == id);
                return Task.CompletedTask;
            }

            public Task<User?> GetByIdAsync(System.Guid id)
            {
                return Task.FromResult(_users.Find(u => u.Id == id));
            }

            public Task<User?> GetByUsernameAsync(string username)
            {
                return Task.FromResult(_users.Find(u => u.Username == username));
            }

            public Task<IEnumerable<User>> ListAllAsync()
            {
                return Task.FromResult<IEnumerable<User>>(_users);
            }

            public Task<int> SaveChangesAsync() => Task.FromResult(0);

            public Task UpdateAsync(User user)
            {
                var idx = _users.FindIndex(u => u.Id == user.Id);
                if (idx >= 0) _users[idx] = user;
                return Task.CompletedTask;
            }
        }

        [TestMethod]
        public async Task CreateUser_PreventsDuplicateUsername()
        {
            var repo = new InMemoryUserRepository();
            var svc = new UserService(repo);

            var created = await svc.CreateUserAsync("bob", "bob@example.com", "Bob");

            await Assert.ThrowsExceptionAsync<System.InvalidOperationException>(async () =>
            {
                await svc.CreateUserAsync("bob", "bob2@example.com", "Bobby");
            });
        }
    }
}
