using Api.Services;

namespace Api.Extensions;

public static class BuildExtensions
{
    public static WebApplication BuildWithOptions(this WebApplicationBuilder builder)
    {
        var app = builder.Build();
       
        app.UseCors("CorsPolicy");
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapGrpcService<UserService>();
        app.MapGrpcService<GroupService>();
        
        return app;
    }
}