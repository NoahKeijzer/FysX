using Microsoft.EntityFrameworkCore.Migrations;

namespace Fysio.Migrations
{
    public partial class addedpackages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Treators",
                columns: new[] { "Email", "Discriminator", "StudentNumber" },
                values: new object[] { "bbuijsen@gmail.com", "Student", 2170769 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Treators",
                keyColumn: "Email",
                keyValue: "bbuijsen@gmail.com");
        }
    }
}
