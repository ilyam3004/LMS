using Api.Common.Mapping;

namespace Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder => builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithExposedHeaders("content-disposition"));
        });

        services.AddMappings();

        services.AddAuthorization();
        services.AddGrpc().AddJsonTranscoding();    

        return services;
    }
}