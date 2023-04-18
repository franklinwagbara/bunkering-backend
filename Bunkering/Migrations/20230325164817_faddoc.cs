using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunkering.Migrations
{
    /// <inheritdoc />
    public partial class faddoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFADDoc",
                table: "FacilityTypeDocuments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFADDoc",
                table: "FacilityTypeDocuments");
        }
    }
}
