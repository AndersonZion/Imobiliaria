using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SistemaImobiliario.Application.Interfaces;
using SistemaImobiliario.Application.Services;
using SistemaImobiliario.Domain.Interfaces;
using SistemaImobiliario.Infra.Repositories;
using SistemaImobiliario.Infrastructure.Data;
using SistemaImobiliario.Infrastructure.Repositories;

namespace SistemaImobiliario.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        return builder;
    }

    public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IInquilineRepository, InquilineRepository>();
        builder.Services.AddScoped<ITenantRepository, TenantRepository>();
        builder.Services.AddScoped<IRenterRepository, RenterRepository>();
        return builder;
    }

    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IInquilinoService, InquilinoService>();
        builder.Services.AddScoped<ITenantService, TenantService>();
        builder.Services.AddScoped<IRenterService, RenterService>();
        return builder;
    }

    public static WebApplicationBuilder AddSwaggerAndCors(this WebApplicationBuilder builder)
    {
        // CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.WithOrigins("http://localhost:5002") // Ajuste conforme front-end
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        // Controllers
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            });

        // Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "SistemaImobiliario API", Version = "v1" });
        });

        return builder;
    }
}
