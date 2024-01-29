using Microsoft.EntityFrameworkCore.Migrations;

namespace GatePassManagementSystem.Migrations
{
    public partial class remoceChngAprl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChAprlId",
                table: "PersonalGP");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChAprlId",
                table: "PersonalGP",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
