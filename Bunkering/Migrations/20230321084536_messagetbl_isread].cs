using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunkering.Migrations
{
    /// <inheritdoc />
    public partial class messagetblisread : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ApplicationId",
                table: "Messages",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Applications_ApplicationId",
                table: "Messages",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Applications_ApplicationId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ApplicationId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Messages");
        }
    }
}
