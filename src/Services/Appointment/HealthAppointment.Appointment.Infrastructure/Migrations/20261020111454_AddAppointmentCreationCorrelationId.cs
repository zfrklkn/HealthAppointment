using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthAppointment.Appointment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentCreationCorrelationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreationCorrelationId",
                table: "appointments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_appointments_CreationCorrelationId",
                table: "appointments",
                column: "CreationCorrelationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_appointments_CreationCorrelationId",
                table: "appointments");

            migrationBuilder.DropColumn(
                name: "CreationCorrelationId",
                table: "appointments");
        }
    }
}
