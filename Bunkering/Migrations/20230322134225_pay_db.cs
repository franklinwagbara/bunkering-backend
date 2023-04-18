using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunkering.Migrations
{
    /// <inheritdoc />
    public partial class paydb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Permits_ApplicationId",
                table: "Permits",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permits_Applications_ApplicationId",
                table: "Permits",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permits_Applications_ApplicationId",
                table: "Permits");

            migrationBuilder.DropIndex(
                name: "IX_Permits_ApplicationId",
                table: "Permits");
        }
    }
}
