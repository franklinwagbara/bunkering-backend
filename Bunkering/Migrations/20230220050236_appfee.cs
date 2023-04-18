using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunkering.Migrations
{
    /// <inheritdoc />
    public partial class appfee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppFees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationTypeId = table.Column<int>(type: "int", nullable: false),
                    FacilityTypeId = table.Column<int>(type: "int", nullable: false),
                    ApplicationFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccreditationFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VesselLicenseFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdministrativeFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SerciveCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppFees_ApplicationTypes_ApplicationTypeId",
                        column: x => x.ApplicationTypeId,
                        principalTable: "ApplicationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppFees_FacilityTypes_FacilityTypeId",
                        column: x => x.FacilityTypeId,
                        principalTable: "FacilityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppFees_ApplicationTypeId",
                table: "AppFees",
                column: "ApplicationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFees_FacilityTypeId",
                table: "AppFees",
                column: "FacilityTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppFees");
        }
    }
}
