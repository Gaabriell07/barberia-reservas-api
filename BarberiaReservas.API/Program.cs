using BarberiaReservas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Application.Services.Mocks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "Barbería Reservas API", 
        Version = "v1",
        Description = "Sistema de gestión de reservas para barbería"
    });
});

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// TODO: TEMPORAL - Reemplazar por implementaciones reales cuando estén listas
// Usuarios
builder.Services.AddScoped<BarberiaReservas.Domain.Interfaces.IUserRepository, BarberiaReservas.Infrastructure.Repositories.UserRepository>();
builder.Services.AddScoped<IUserValidator, BarberiaReservas.Application.Services.UserValidator>();
builder.Services.AddScoped<BarberiaReservas.Application.Interfaces.IRoleManager, BarberiaReservas.Application.Services.RoleManager>();
builder.Services.AddScoped<IUserService, BarberiaReservas.Application.Services.UserService>();
var app = builder.Build();

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
