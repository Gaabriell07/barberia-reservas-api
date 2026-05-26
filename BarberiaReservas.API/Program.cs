using BarberiaReservas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Application.Services;
using BarberiaReservas.Application.Services.Mocks;
using BarberiaReservas.Domain.Interfaces; // Para IUserRepository

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
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

builder.Services.AddScoped<IUserRepository, BarberiaReservas.Infrastructure.Repositories.UserRepository>();
builder.Services.AddScoped<IUserValidator, BarberiaReservas.Application.Services.UserValidator>();
builder.Services.AddScoped<IRoleManager, BarberiaReservas.Application.Services.RoleManager>();
builder.Services.AddScoped<IUserService, BarberiaReservas.Application.Services.UserService>();

builder.Services.AddScoped<BarberiaReservas.Domain.Interfaces.IReservationRepository, BarberiaReservas.Infrastructure.Repositories.ReservationRepository>();
builder.Services.AddScoped<BarberiaReservas.Application.Interfaces.IReservationService, BarberiaReservas.Application.Services.ReservationService>();

builder.Services.AddScoped<IAuthService, BarberiaReservas.Application.Services.AuthService>();
builder.Services.AddScoped<ITokenGenerator, BarberiaReservas.Application.Services.JwtTokenGenerator>();
builder.Services.AddScoped<IPasswordHasher, BarberiaReservas.Application.Services.PasswordHasher>();

builder.Services.AddScoped<IAvailabilityService, MockAvailabilityService>();
builder.Services.AddScoped<IServiceManager, MockServiceManager>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ITemplateManager, TemplateManager>();
builder.Services.AddScoped<INotificationChannel, EmailChannel>();
builder.Services.AddScoped<INotificationChannel, SmsChannel>();

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
