using Microsoft.AspNetCore.Builder;

namespace SistemaImobiliario.API.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseApiPipeline(this WebApplication app)
    {
        // Swagger no ambiente de desenvolvimento
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SistemaImobiliario API V1");
            });
        }

        app.UseHttpsRedirection();

        // CORS
        app.UseCors("AllowAll");

        app.UseAuthorization();

        // Mapear controllers
        app.MapControllers();

        return app;
    }
}
