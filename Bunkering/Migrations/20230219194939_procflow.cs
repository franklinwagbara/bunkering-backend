using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunkering.Migrations
{
    /// <inheritdoc />
    public partial class procflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "WorkFlows",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FacilityTypeId",
                table: "WorkFlows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Rate",
                table: "WorkFlows",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TargetRole",
                table: "WorkFlows",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TriggeredByRole",
                table: "WorkFlows",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "WorkFlows");

            migrationBuilder.DropColumn(
                name: "FacilityTypeId",
                table: "WorkFlows");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "WorkFlows");

            migrationBuilder.DropColumn(
                name: "TargetRole",
                table: "WorkFlows");

            migrationBuilder.DropColumn(
                name: "TriggeredByRole",
                table: "WorkFlows");
        }
    }
}
