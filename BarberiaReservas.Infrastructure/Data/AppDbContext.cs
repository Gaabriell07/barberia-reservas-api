using BarberiaReservas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberiaReservas.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<WorkingHours> WorkingHours { get; set; }
    public DbSet<BlockedDate> BlockedDates { get; set; }
    public DbSet<NotificationLog> NotificationLogs { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.PasswordHash).IsRequired();
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Price).HasColumnType("decimal(10,2)");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e => e.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(e => e.Barber)
                .WithMany()
                .HasForeignKey(e => e.BarberId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(e => e.Service)
                .WithMany(s => s.Reservations)
                .HasForeignKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Notes).HasMaxLength(500);
        });

        modelBuilder.Entity<WorkingHours>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DayOfWeek).IsRequired().HasMaxLength(20);
        });

        modelBuilder.Entity<BlockedDate>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Date);
            entity.Property(e => e.Reason).HasMaxLength(200);
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Token).IsRequired().HasMaxLength(200);
            entity.HasOne(e => e.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        SeedData(modelBuilder);
    }
    
    private void SeedData(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Name = "Administrador",
                Email = "admin@barberia.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Role = "Admin",
                Phone = "999888777",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            }
        );

        modelBuilder.Entity<Service>().HasData(
            new Service 
            { 
                Id = 1, 
                Name = "Corte Clásico", 
                Description = "Corte de cabello tradicional con tijera y máquina",
                Price = 15.00m, 
                DurationMinutes = 30,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Service 
            { 
                Id = 2, 
                Name = "Barba", 
                Description = "Arreglo y perfilado de barba",
                Price = 10.00m, 
                DurationMinutes = 20,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Service 
            { 
                Id = 3, 
                Name = "Corte + Barba", 
                Description = "Combo completo de corte y barba",
                Price = 20.00m, 
                DurationMinutes = 45,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Service 
            { 
                Id = 4, 
                Name = "Afeitado Clásico", 
                Description = "Afeitado con navaja y toalla caliente",
                Price = 12.00m, 
                DurationMinutes = 25,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        );

        var days = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        for (int i = 0; i < days.Length; i++)
        {
            modelBuilder.Entity<WorkingHours>().HasData(
                new WorkingHours
                {
                    Id = i + 1,
                    DayOfWeek = days[i],
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(18, 0, 0),
                    IsActive = true
                }
            );
        }
    }
}
