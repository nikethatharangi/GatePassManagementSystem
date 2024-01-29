using Microsoft.EntityFrameworkCore.Migrations;

namespace GatePassManagementSystem.Migrations
{
    public partial class AddPGFKToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepId",
                table: "PersonalGP",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalGP_DepId",
                table: "PersonalGP",
                column: "DepId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalGP_Department_DepId",
                table: "PersonalGP",
                column: "DepId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalGP_Department_DepId",
                table: "PersonalGP");

            migrationBuilder.DropIndex(
                name: "IX_PersonalGP_DepId",
                table: "PersonalGP");

            migrationBuilder.DropColumn(
                name: "DepId",
                table: "PersonalGP");
        }
    }
}
