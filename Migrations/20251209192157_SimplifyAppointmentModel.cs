using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grosu_Andrada_ClinicAppointments.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyAppointmentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentKaggleId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "NoShow",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "NoShowRiskScore",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "ScheduledDate",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "SmsReceived",
                table: "Appointment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppointmentKaggleId",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "NoShow",
                table: "Appointment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "NoShowRiskScore",
                table: "Appointment",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledDate",
                table: "Appointment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "SmsReceived",
                table: "Appointment",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
