using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunkering.Migrations
{
    /// <inheritdoc />
    public partial class appointmentdetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDate",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ApprovalMessage",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClientMessage",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNo",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduleDate",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ScheduleMessage",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ScheduledBy",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SubmittedDocuments_ApplicationId",
                table: "SubmittedDocuments",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmittedDocuments_Applications_ApplicationId",
                table: "SubmittedDocuments",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubmittedDocuments_Applications_ApplicationId",
                table: "SubmittedDocuments");

            migrationBuilder.DropIndex(
                name: "IX_SubmittedDocuments_ApplicationId",
                table: "SubmittedDocuments");

            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ApprovalMessage",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ClientMessage",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PhoneNo",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ScheduleDate",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ScheduleMessage",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ScheduledBy",
                table: "Appointments");
        }
    }
}
