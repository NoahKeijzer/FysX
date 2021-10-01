using Microsoft.EntityFrameworkCore.Migrations;

namespace Fysio.Migrations
{
    public partial class smallchangesandmadetreatmentplanpublic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "complaints",
                table: "PatientFiles",
                newName: "Complaints");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Treators",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinutesPerSession",
                table: "TreatmentPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TreatmentsPerWeek",
                table: "TreatmentPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Treators",
                keyColumn: "Email",
                keyValue: "bbuijsen@gmail.com",
                column: "Name",
                value: "Bas Buijsen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Treators");

            migrationBuilder.DropColumn(
                name: "MinutesPerSession",
                table: "TreatmentPlans");

            migrationBuilder.DropColumn(
                name: "TreatmentsPerWeek",
                table: "TreatmentPlans");

            migrationBuilder.RenameColumn(
                name: "Complaints",
                table: "PatientFiles",
                newName: "complaints");
        }
    }
}
