using Goal.Infrastructure.Mappings;
using Goal.Infrastructure.Persistence;
using Goal.Infrastructure.Repositories.Interfaces.Product;
using Goal.Infrastructure.Repositories.Services.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Goal.Infrastructure;

public static class DbExtensions
{
    public static IServiceCollection AddDbExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GoalDatabaseContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"),
                sqlOptions => sqlOptions.EnableRetryOnFailure()
            );
            options.LogTo(Console.WriteLine);
        });

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddSingleton<IInfrastructureMapper, InfrastructureMapper>();

        return services;
    }
}