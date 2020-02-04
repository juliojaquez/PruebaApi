using Microsoft.EntityFrameworkCore.Migrations;

namespace PruebaAPI.Migrations
{
    public partial class removeProviderId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Provider",
                table: "Receipts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Provider",
                table: "Receipts");
        }
    }
}
