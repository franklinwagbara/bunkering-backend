using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunkering.Migrations
{
    /// <inheritdoc />
    public partial class flowidnull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_WorkFlows_FlowId",
                table: "Applications");

            migrationBuilder.AlterColumn<int>(
                name: "FlowId",
                table: "Applications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_WorkFlows_FlowId",
                table: "Applications",
                column: "FlowId",
                principalTable: "WorkFlows",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_WorkFlows_FlowId",
                table: "Applications");

            migrationBuilder.AlterColumn<int>(
                name: "FlowId",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_WorkFlows_FlowId",
                table: "Applications",
                column: "FlowId",
                principalTable: "WorkFlows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
