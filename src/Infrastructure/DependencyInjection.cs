using Microsoft.AspNetCore.Authentication.JwtBearer;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Application.Services;
using System.Text;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddAuth(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddDbContext<LmsDbContext>(options =>
            options.UseNpgsql(configuration
                .GetConnectionString("DefaultConnection")));

        services.AddTransient<IUnitOfWork, UnitOfWork>();
        MigrateDatabase(services);

        return services;
    }

    private static void AddAuth(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        JwtSettings jwtSettings = new();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IJwtTokenReader, JwtTokenReader>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(jwtSettings.Secret)),
            });
    }

    private static void MigrateDatabase(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<LmsDbContext>();

        dbContext.Database.Migrate();
    }
}