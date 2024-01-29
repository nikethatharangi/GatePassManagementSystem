using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GatePassManagementSystem.Migrations
{
    public partial class AddStColsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ASdgmtime",
                table: "PersonalGP",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ASgmtime",
                table: "PersonalGP",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "AShodtime",
                table: "PersonalGP",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ASmdtime",
                table: "PersonalGP",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RejctReason",
                table: "PersonalGP",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ASdgmtime",
                table: "PersonalGP");

            migrationBuilder.DropColumn(
                name: "ASgmtime",
                table: "PersonalGP");

            migrationBuilder.DropColumn(
                name: "AShodtime",
                table: "PersonalGP");

            migrationBuilder.DropColumn(
                name: "ASmdtime",
                table: "PersonalGP");

            migrationBuilder.DropColumn(
                name: "RejctReason",
                table: "PersonalGP");
        }
    }
}
