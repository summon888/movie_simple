using Domain.Core.Providers.Hash;

namespace WebAPI.StartupExtensions
{
    public static class HashingExtension
    {
        public static IServiceCollection AddCustomizedHash(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<HashingOptions>(configuration.GetSection(HashingOptions.Hashing));
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
