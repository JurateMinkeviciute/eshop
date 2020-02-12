using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCoreProject.Migrations
{
    public partial class AddedToPhoneOneColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Phones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Phones");
        }
    }
}
