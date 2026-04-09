using PetCareApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agrega servicios de controladores
builder.Services.AddControllers();

// Configuración de base de datos en memoria (rápido para demo)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=petcare.db"));

// Swagger para documentar API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();