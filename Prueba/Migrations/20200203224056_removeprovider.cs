using Microsoft.EntityFrameworkCore.Migrations;

namespace PruebaAPI.Migrations
{
    public partial class removeprovider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Provider_ProviderId",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_ProviderId",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Receipts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProviderId",
                table: "Receipts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_ProviderId",
                table: "Receipts",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Provider_ProviderId",
                table: "Receipts",
                column: "ProviderId",
                principalTable: "Provider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
