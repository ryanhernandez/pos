using Server.Modules.Users.Domain;

namespace Server.Modules.Users.GraphQL
{
    public class UserQueries
    {
        private readonly UserService _service;

        public UserQueries(UserService service)
        {
            _service = service;
        }

        public Task<IEnumerable<User>> GetUsers() => _service.ListUsersAsync();

        public Task<User?> GetUser(Guid id) => _service.GetUserAsync(id);
    }
}
