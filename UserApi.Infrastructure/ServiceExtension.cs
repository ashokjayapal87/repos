using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserApi.Core.Interfaces;
using UserApi.Core.Validations;
using UserApi.Infrastructure.Repositories;
using UserApi.Models;

namespace UserApi.Infrastructure;

public static class ServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DbContextClass>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            .EnableSensitiveDataLogging(true);
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddValidatorsFromAssemblyContaining<UserValidator>();

        return services;
    }
}
