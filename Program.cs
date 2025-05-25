using Microsoft.EntityFrameworkCore;
using MottuApi.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext
builder.Services.AddDbContext<MottuDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Configuração dos controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configuração para preservar referências e evitar erros de ciclo
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

// Configuração do Swagger para documentação da API
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mottu API",
        Version = "v1",
        Description = "API para gestão de motos, funcionários, pátios e clientes."
    });

    options.TagActionsBy(api => new[] { api.ActionDescriptor.RouteValues["controller"] });
});

var app = builder.Build();

// Habilita o Swagger e SwaggerUI em qualquer ambiente
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mottu API v1");
    c.RoutePrefix = string.Empty; // Swagger acessível em "/"
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
