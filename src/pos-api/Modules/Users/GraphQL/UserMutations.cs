using Server.Modules.Users.Domain;

namespace Server.Modules.Users.GraphQL
{
    public class CreateUserInput
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? DisplayName { get; set; }
    }

    public class UpdateUserInput
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? DisplayName { get; set; }
    }

    public class UserMutations
    {
        private readonly UserService _service;

        public UserMutations(UserService service)
        {
            _service = service;
        }

        public Task<User> CreateUser(CreateUserInput input) => _service.CreateUserAsync(input.Username, input.Email, input.DisplayName);

        public Task<User?> UpdateUser(UpdateUserInput input) => _service.UpdateUserAsync(input.Id, input.Username, input.Email, input.DisplayName);

        public Task<bool> DeleteUser(Guid id) => _service.DeleteUserAsync(id);
    }
}
