using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthAppointment.Orchestrator.Migrations
{
    /// <inheritdoc />
    public partial class InitialOrchestratorSaga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentCreationStates",
                columns: table => new
                {
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentState = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
                    SpecialtyId = table.Column<Guid>(type: "uuid", nullable: true),
                    AppointmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Reason = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    FailureReason = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentCreationStates", x => x.CorrelationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentCreationStates_CreatedAtUtc",
                table: "AppointmentCreationStates",
                column: "CreatedAtUtc");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentCreationStates_CurrentState",
                table: "AppointmentCreationStates",
                column: "CurrentState");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentCreationStates");
        }
    }
}
