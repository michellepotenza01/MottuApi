using Microsoft.EntityFrameworkCore;
using MottuApi.Data;
using MottuApi.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do DbContext
builder.Services.AddDbContext<MottuDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Configura��o dos controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configura��o para preservar refer�ncias e evitar erros de ciclo
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

// Configura��o do Swagger para documenta��o da API
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mottu API",
        Version = "v1",
        Description = "API para gest�o de motos, funcion�rios, p�tios e clientes."
    });
});

// Aqui configuramos o Kestrel diretamente para escutar na porta 80
builder.WebHost.UseUrls("http://0.0.0.0:80");  // Aplique isso para escutar na porta 80 de todas as interfaces

var app = builder.Build();

// Configura��o do Swagger UI para visualiza��o e testes
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
