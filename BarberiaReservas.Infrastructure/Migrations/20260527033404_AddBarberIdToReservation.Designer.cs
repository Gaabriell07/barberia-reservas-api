
using System;
using BarberiaReservas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BarberiaReservas.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20260527033404_AddBarberIdToReservation")]
    partial class AddBarberIdToReservation
    {
        
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BarberiaReservas.Domain.Entities.BlockedDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Date");

                    b.ToTable("BlockedDates");
                });

            modelBuilder.Entity("BarberiaReservas.Domain.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BarberId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BarberId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("BarberiaReservas.Domain.Entities.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("DurationMinutes")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.ToTable("Services");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2026, 5, 27, 3, 34, 3, 159, DateTimeKind.Utc).AddTicks(4290),
                            Description = "Corte de cabello tradicional con tijera y máquina",
                            DurationMinutes = 30,
                            IsActive = true,
                            Name = "Corte Clásico",
                            Price = 15.00m
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2026, 5, 27, 3, 34, 3, 159, DateTimeKind.Utc).AddTicks(4293),
                            Description = "Arreglo y perfilado de barba",
                            DurationMinutes = 20,
                            IsActive = true,
                            Name = "Barba",
                            Price = 10.00m
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2026, 5, 27, 3, 34, 3, 159, DateTimeKind.Utc).AddTicks(4295),
                            Description = "Combo completo de corte y barba",
                            DurationMinutes = 45,
                            IsActive = true,
                            Name = "Corte + Barba",
                            Price = 20.00m
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2026, 5, 27, 3, 34, 3, 159, DateTimeKind.Utc).AddTicks(4297),
                            Description = "Afeitado con navaja y toalla caliente",
                            DurationMinutes = 25,
                            IsActive = true,
                            Name = "Afeitado Clásico",
                            Price = 12.00m
                        });
                });

            modelBuilder.Entity("BarberiaReservas.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2026, 5, 27, 3, 34, 3, 159, DateTimeKind.Utc).AddTicks(3496),
                            Email = "admin@barberia.com",
                            IsActive = true,
                            Name = "Administrador",
                            PasswordHash = "$2a$11$o37CBvXNyjTlWl3uEuPz1uAKkPLR9qOyQ1nNl8/l0TX6PoWBglL7e",
                            Phone = "999888777",
                            Role = "Admin"
                        });
                });

            modelBuilder.Entity("BarberiaReservas.Domain.Entities.WorkingHours", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DayOfWeek")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("WorkingHours");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DayOfWeek = "Monday",
                            EndTime = new TimeSpan(0, 18, 0, 0, 0),
                            IsActive = true,
                            StartTime = new TimeSpan(0, 9, 0, 0, 0)
                        },
                        new
                        {
                            Id = 2,
                            DayOfWeek = "Tuesday",
                            EndTime = new TimeSpan(0, 18, 0, 0, 0),
                            IsActive = true,
                            StartTime = new TimeSpan(0, 9, 0, 0, 0)
                        },
                        new
                        {
                            Id = 3,
                            DayOfWeek = "Wednesday",
                            EndTime = new TimeSpan(0, 18, 0, 0, 0),
                            IsActive = true,
                            StartTime = new TimeSpan(0, 9, 0, 0, 0)
                        },
                        new
                        {
                            Id = 4,
                            DayOfWeek = "Thursday",
                            EndTime = new TimeSpan(0, 18, 0, 0, 0),
                            IsActive = true,
                            StartTime = new TimeSpan(0, 9, 0, 0, 0)
                        },
                        new
                        {
                            Id = 5,
                            DayOfWeek = "Friday",
                            EndTime = new TimeSpan(0, 18, 0, 0, 0),
                            IsActive = true,
                            StartTime = new TimeSpan(0, 9, 0, 0, 0)
                        },
                        new
                        {
                            Id = 6,
                            DayOfWeek = "Saturday",
                            EndTime = new TimeSpan(0, 18, 0, 0, 0),
                            IsActive = true,
                            StartTime = new TimeSpan(0, 9, 0, 0, 0)
                        });
                });

            modelBuilder.Entity("BarberiaReservas.Domain.Entities.Reservation", b =>
                {
                    b.HasOne("BarberiaReservas.Domain.Entities.User", "Barber")
                        .WithMany()
                        .HasForeignKey("BarberId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BarberiaReservas.Domain.Entities.Service", "Service")
                        .WithMany("Reservations")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BarberiaReservas.Domain.Entities.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Barber");

                    b.Navigation("Service");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BarberiaReservas.Domain.Entities.Service", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("BarberiaReservas.Domain.Entities.User", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
