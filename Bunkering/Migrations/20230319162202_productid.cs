using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunkering.Migrations
{
    /// <inheritdoc />
    public partial class productid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Tanks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tanks_ProductId",
                table: "Tanks",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tanks_Products_ProductId",
                table: "Tanks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tanks_Products_ProductId",
                table: "Tanks");

            migrationBuilder.DropIndex(
                name: "IX_Tanks_ProductId",
                table: "Tanks");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Tanks");
        }
    }
}
