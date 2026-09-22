using Microsoft.EntityFrameworkCore;
using exercicios.Data;
using exercicios.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuração de Serviços
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Configuração do DbContext com SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=app.db")
);

// Injeção de Dependência do Serviço de Livro
builder.Services.AddScoped<ILivroService, LivroService>();

var app = builder.Build();

// Pipeline de Requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();