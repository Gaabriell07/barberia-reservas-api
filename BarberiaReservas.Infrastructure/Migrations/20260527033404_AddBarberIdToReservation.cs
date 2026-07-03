using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberiaReservas.Infrastructure.Migrations
{
    
    public partial class AddBarberIdToReservation : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BarberId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 27, 3, 34, 3, 159, DateTimeKind.Utc).AddTicks(4290));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 27, 3, 34, 3, 159, DateTimeKind.Utc).AddTicks(4293));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 27, 3, 34, 3, 159, DateTimeKind.Utc).AddTicks(4295));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 27, 3, 34, 3, 159, DateTimeKind.Utc).AddTicks(4297));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 27, 3, 34, 3, 159, DateTimeKind.Utc).AddTicks(3496), "$2a$11$o37CBvXNyjTlWl3uEuPz1uAKkPLR9qOyQ1nNl8/l0TX6PoWBglL7e" });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_BarberId",
                table: "Reservations",
                column: "BarberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_BarberId",
                table: "Reservations",
                column: "BarberId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_BarberId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_BarberId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "BarberId",
                table: "Reservations");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 20, 30, 41, 280, DateTimeKind.Utc).AddTicks(1376));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 20, 30, 41, 280, DateTimeKind.Utc).AddTicks(1378));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 20, 30, 41, 280, DateTimeKind.Utc).AddTicks(1379));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 20, 30, 41, 280, DateTimeKind.Utc).AddTicks(1381));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 13, 20, 30, 41, 280, DateTimeKind.Utc).AddTicks(644), "$2a$11$cutXrF.tmnS6KMXJ8g.qnO27ZhdhPIBansA7OQnpa6TLDY4DYbp96" });
        }
    }
}
