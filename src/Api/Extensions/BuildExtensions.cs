namespace Api.Extensions;

public static class BuildExtensions
{

    public static WebApplication BuildWithOptions(this WebApplicationBuilder builder)
    {
        var app = builder.Build();
       
        app.UseCors("CorsPolicy");

        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        
        return app;
    }
}