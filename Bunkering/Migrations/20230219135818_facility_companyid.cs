using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunkering.Migrations
{
    /// <inheritdoc />
    public partial class facilitycompanyid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Facilities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_CompanyId",
                table: "Facilities",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_Companies_CompanyId",
                table: "Facilities",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_Companies_CompanyId",
                table: "Facilities");

            migrationBuilder.DropIndex(
                name: "IX_Facilities_CompanyId",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Facilities");
        }
    }
}
