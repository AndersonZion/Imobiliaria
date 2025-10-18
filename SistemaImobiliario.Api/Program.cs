using SistemaImobiliario.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços de forma modular
builder
    .AddDatabase()
    .AddRepositories()
    .AddApplicationServices()
    .AddSwaggerAndCors();

var app = builder.Build();


// Pipeline HTTP modular
app.UseApiPipeline();

app.Run();
