using Microsoft.EntityFrameworkCore;
using MottuApi.Data;
using MottuApi.Models;
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
});

// Aqui configuramos o Kestrel diretamente para escutar na porta 80
builder.WebHost.UseUrls("http://0.0.0.0:80");  // Aplique isso para escutar na porta 80 de todas as interfaces

var app = builder.Build();

// Configuração do Swagger UI para visualização e testes
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mottu API v1");
        c.RoutePrefix = string.Empty;  // Acesso ao Swagger diretamente na raiz
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
