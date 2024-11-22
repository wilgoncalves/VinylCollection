using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using Serilog;
using VinylCollection.Interfaces;
using VinylCollection.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Serilog para logar em um arquivo
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");

// Registrar a conexão como um serviço singleton (abre a conexão uma vez)
builder.Services.AddSingleton<IDbConnection>(sp => new MySqlConnection(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITitleRepository>();
builder.Services.AddScoped<TitleRepository>();

var app = builder.Build();

using (var connection = app.Services.GetRequiredService<IDbConnection>())
{
    try
    {
        // Tenta abrir a conexão com o banco de dados
        connection.Open();
    }
    catch (Exception ex)
    {
        // Logando o erro usando o Serilog
        Log.Error($"[{DateTime.Now}] Erro ao conectar ao banco de dados: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
