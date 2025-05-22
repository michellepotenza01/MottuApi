using Microsoft.EntityFrameworkCore;
using MottuApi.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD
builder.Services.AddDbContext<MottuDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
=======
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
>>>>>>> 62f9b743ffb9732fdc1a0ab84ee219aab1dbd018
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mottu API",
        Version = "v1",
        Description = "API para gestão de motos, funcionários, pátios e clientes."
    });

<<<<<<< HEAD
    options.TagActionsBy(api => [api.ActionDescriptor.RouteValues["controller"]]);
=======
// Aqui configuramos o Kestrel diretamente para escutar na porta 80
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);  // Configura Kestrel para escutar na porta 80 em todas as interfaces
>>>>>>> 62f9b743ffb9732fdc1a0ab84ee219aab1dbd018
});

var app = builder.Build();

<<<<<<< HEAD
=======
// Configuração do Swagger UI para visualização e testes
>>>>>>> 62f9b743ffb9732fdc1a0ab84ee219aab1dbd018
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