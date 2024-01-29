using Microsoft.EntityFrameworkCore.Migrations;

namespace GatePassManagementSystem.Migrations
{
    public partial class AddWGPchk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ChkHalfd",
                table: "WorkerGP",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ChkLunch",
                table: "WorkerGP",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ChkMadu",
                table: "WorkerGP",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ChkOther",
                table: "WorkerGP",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ChkShrt",
                table: "WorkerGP",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ChkSinthawatta",
                table: "WorkerGP",
                type: "bit",
                nullable: true,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChkHalfd",
                table: "WorkerGP");

            migrationBuilder.DropColumn(
                name: "ChkLunch",
                table: "WorkerGP");

            migrationBuilder.DropColumn(
                name: "ChkMadu",
                table: "WorkerGP");

            migrationBuilder.DropColumn(
                name: "ChkOther",
                table: "WorkerGP");

            migrationBuilder.DropColumn(
                name: "ChkShrt",
                table: "WorkerGP");

            migrationBuilder.DropColumn(
                name: "ChkSinthawatta",
                table: "WorkerGP");
        }
    }
}
