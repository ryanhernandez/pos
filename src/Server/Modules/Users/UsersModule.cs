using Microsoft.Extensions.DependencyInjection;
using Server.Modules.Users.Domain;
using Server.Modules.Users.Infrastructure;
using Server.Modules.Users.GraphQL;

namespace Server.Modules.Users
{
    public static class UsersModule
    {
        // Registers Users services, repository and GraphQL types.
        public static IServiceCollection AddUsersModule(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<UserService>();

            services.AddGraphQLServer()
                .AddQueryType<UserQueries>()
                .AddMutationType<UserMutations>();

            return services;
        }
    }
}
