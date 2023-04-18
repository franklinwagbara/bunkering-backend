using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunkering.Migrations
{
    /// <inheritdoc />
    public partial class tanks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_States_StateId",
                table: "Facilities");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "Facilities",
                newName: "LgaId");

            migrationBuilder.RenameIndex(
                name: "IX_Facilities_StateId",
                table: "Facilities",
                newName: "IX_Facilities_LgaId");

            migrationBuilder.AddColumn<int>(
                name: "ElpsId",
                table: "Facilities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Tanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tanks_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tanks_FacilityId",
                table: "Tanks",
                column: "FacilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_LGAs_LgaId",
                table: "Facilities",
                column: "LgaId",
                principalTable: "LGAs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_LGAs_LgaId",
                table: "Facilities");

            migrationBuilder.DropTable(
                name: "Tanks");

            migrationBuilder.DropColumn(
                name: "ElpsId",
                table: "Facilities");

            migrationBuilder.RenameColumn(
                name: "LgaId",
                table: "Facilities",
                newName: "StateId");

            migrationBuilder.RenameIndex(
                name: "IX_Facilities_LgaId",
                table: "Facilities",
                newName: "IX_Facilities_StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_States_StateId",
                table: "Facilities",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
