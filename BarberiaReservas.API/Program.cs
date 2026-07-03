using BarberiaReservas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Application.Services;
using BarberiaReservas.Domain.Interfaces; 
using BarberiaReservas.Infrastructure.Repositories;

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

builder.Services.AddScoped<IServiceRepository, ServiceRepository>(
    
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

builder.Services.AddScoped<IAuthService, BarberiaReservas.Application.Services.AuthService>();
builder.Services.AddScoped<ITokenGenerator, BarberiaReservas.Application.Services.JwtTokenGenerator>();
builder.Services.AddScoped<IPasswordHasher, BarberiaReservas.Application.Services.PasswordHasher>();

builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ITemplateManager, TemplateManager>();
builder.Services.AddScoped<INotificationChannel, EmailChannel>();
builder.Services.AddScoped<INotificationChannel, SmsChannel>();

builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IReservationValidator, ReservationValidator>();
builder.Services.AddScoped<IReservationStateManager, ReservationStateManager>();
builder.Services.AddScoped<IReservationRepository, BarberiaReservas.Infrastructure.Repositories.ReservationRepository>();

builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();
builder.Services.AddScoped<IWorkingHoursManager, WorkingHoursManager>();
builder.Services.AddScoped<ITimeSlotGenerator, TimeSlotGenerator>();

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
