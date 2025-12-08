using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grosu_Andrada_ClinicAppointments.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatientSimplified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alcoholism",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Diabetes",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Handicap",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Hipertension",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PatientKaggleId",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Scholarship",
                table: "Patient");

            migrationBuilder.RenameColumn(
                name: "Neighbourhood",
                table: "Patient",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Patient");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Patient",
                newName: "Neighbourhood");

            migrationBuilder.AddColumn<bool>(
                name: "Alcoholism",
                table: "Patient",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Diabetes",
                table: "Patient",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Handicap",
                table: "Patient",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Hipertension",
                table: "Patient",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "PatientKaggleId",
                table: "Patient",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "Scholarship",
                table: "Patient",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
