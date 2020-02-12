using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCoreProject.Migrations
{
    public partial class ChangedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Phones",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "OldPrice",
                table: "Phones",
                nullable: true,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Phones",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OldPrice",
                table: "Phones",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
