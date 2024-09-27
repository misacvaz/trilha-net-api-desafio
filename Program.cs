using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados MySQL
builder.Services.AddDbContext<OrganizadorContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ConexaoPadrao"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ConexaoPadrao"))));

// Adiciona suporte para controladores com API e Views (MVC)
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Mapear controladores para API e MVC
app.MapControllers(); // Para a API (TarefaController)
//app.MapDefaultControllerRoute(); // Para as rotas MVC (TarefaMVCController)

app.Run();
