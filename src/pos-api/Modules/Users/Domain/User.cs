using Server.Shared.Kernel;

namespace Server.Modules.Users.Domain
{
    public class User : EntityBase
    {
        public string Username { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string? DisplayName { get; private set; }

        // EF Core requires parameterless constructor
        private User() { }

        public User(string username, string email, string? displayName = null)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username is required", nameof(username));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required", nameof(email));

            Username = username;
            Email = email;
            DisplayName = displayName;
        }

        public void Update(string username, string email, string? displayName = null)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username is required", nameof(username));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required", nameof(email));

            Username = username;
            Email = email;
            DisplayName = displayName;
        }
    }
}
