using Microsoft.EntityFrameworkCore.Migrations;

namespace GatePassManagementSystem.Migrations
{
    public partial class AddPGPchk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ChkHalfd",
                table: "PersonalGP",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ChkLunch",
                table: "PersonalGP",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ChkMadu",
                table: "PersonalGP",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ChkOther",
                table: "PersonalGP",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ChkShrt",
                table: "PersonalGP",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ChkSinthawatta",
                table: "PersonalGP",
                type: "bit",
                nullable: true,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChkHalfd",
                table: "PersonalGP");

            migrationBuilder.DropColumn(
                name: "ChkLunch",
                table: "PersonalGP");

            migrationBuilder.DropColumn(
                name: "ChkMadu",
                table: "PersonalGP");

            migrationBuilder.DropColumn(
                name: "ChkOther",
                table: "PersonalGP");

            migrationBuilder.DropColumn(
                name: "ChkShrt",
                table: "PersonalGP");

            migrationBuilder.DropColumn(
                name: "ChkSinthawatta",
                table: "PersonalGP");
        }
    }
}
