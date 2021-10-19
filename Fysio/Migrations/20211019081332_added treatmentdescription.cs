using Microsoft.EntityFrameworkCore.Migrations;

namespace Fysio.Migrations
{
    public partial class addedtreatmentdescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypeDescription",
                table: "Treatments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeDescription",
                table: "Treatments");
        }
    }
}
