using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunkering.Migrations
{
    /// <inheritdoc />
    public partial class permit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_WorkFlows_StageId",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "StageId",
                table: "Applications",
                newName: "FlowId");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_StageId",
                table: "Applications",
                newName: "IX_Applications_FlowId");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastJobDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Permits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ElpsId = table.Column<int>(type: "int", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PermitNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permits", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_WorkFlows_FlowId",
                table: "Applications",
                column: "FlowId",
                principalTable: "WorkFlows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_WorkFlows_FlowId",
                table: "Applications");

            migrationBuilder.DropTable(
                name: "Permits");

            migrationBuilder.DropColumn(
                name: "LastJobDate",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "FlowId",
                table: "Applications",
                newName: "StageId");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_FlowId",
                table: "Applications",
                newName: "IX_Applications_StageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_WorkFlows_StageId",
                table: "Applications",
                column: "StageId",
                principalTable: "WorkFlows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
