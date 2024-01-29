using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GatePassManagementSystem.Migrations
{
    public partial class AddAStimeColsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ASdgmtime",
                table: "WorkerGP",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ASgmtime",
                table: "WorkerGP",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "AShodtime",
                table: "WorkerGP",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ASmdtime",
                table: "WorkerGP",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RejctReason",
                table: "WorkerGP",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SinIntime",
                table: "WorkerGP",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SinOuttime",
                table: "WorkerGP",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ASdgmtime",
                table: "WorkerGP");

            migrationBuilder.DropColumn(
                name: "ASgmtime",
                table: "WorkerGP");

            migrationBuilder.DropColumn(
                name: "AShodtime",
                table: "WorkerGP");

            migrationBuilder.DropColumn(
                name: "ASmdtime",
                table: "WorkerGP");

            migrationBuilder.DropColumn(
                name: "RejctReason",
                table: "WorkerGP");

            migrationBuilder.DropColumn(
                name: "SinIntime",
                table: "WorkerGP");

            migrationBuilder.DropColumn(
                name: "SinOuttime",
                table: "WorkerGP");
        }
    }
}
