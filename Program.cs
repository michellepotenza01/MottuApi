using Microsoft.EntityFrameworkCore;
using MottuApi.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Recupera a senha do banco de dados da variável de ambiente
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

// Verifica se a variável de ambiente está definida
if (string.IsNullOrEmpty(dbPassword))
{
    throw new InvalidOperationException("A variável de ambiente 'DB_PASSWORD' não foi definida.");
}

// Obtém a string de conexão do arquivo de configuração
var connectionString = builder.Configuration.GetConnectionString("OracleConnection");

// Verifica se a string de conexão foi encontrada no arquivo de configuração
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conexão 'OracleConnection' não foi encontrada.");
}

// Substitui a variável de ambiente na string de conexão
connectionString = connectionString.Replace("${DB_PASSWORD}", dbPassword);

// Configura o DbContext com a string de conexão
builder.Services.AddDbContext<MottuDbContext>(options =>
    options.UseOracle(connectionString));

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
await app.RunAsync();
