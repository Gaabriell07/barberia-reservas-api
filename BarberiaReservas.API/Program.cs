using BarberiaReservas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Application.Services.Mocks;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddScoped<IAvailabilityService, MockAvailabilityService>();
builder.Services.AddScoped<INotificationService, MockNotificationService>();
builder.Services.AddScoped<IServiceManager, MockServiceManager>();
builder.Services.AddScoped<IUserService, MockUserService>();

builder.Services.AddScoped<IAuthService, BarberiaReservas.Application.Services.AuthService>();
builder.Services.AddScoped<ITokenGenerator, BarberiaReservas.Application.Services.JwtTokenGenerator>();
builder.Services.AddScoped<IPasswordHasher, BarberiaReservas.Application.Services.PasswordHasher>();

var app = builder.Build();

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
