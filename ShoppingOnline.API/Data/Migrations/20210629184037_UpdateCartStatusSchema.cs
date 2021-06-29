using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingOnline.API.Data.Migrations
{
    public partial class UpdateCartStatusSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CartStatus_Id",
                table: "CartStatus",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CartStatus_Id",
                table: "CartStatus");
        }
    }
}
