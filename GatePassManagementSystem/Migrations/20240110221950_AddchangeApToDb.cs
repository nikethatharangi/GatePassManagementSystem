using Microsoft.EntityFrameworkCore.Migrations;

namespace GatePassManagementSystem.Migrations
{
    public partial class AddchangeApToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChId",
                table: "PersonalGP",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalGP_ChId",
                table: "PersonalGP",
                column: "ChId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalGP_ApprovalChange_ChId",
                table: "PersonalGP",
                column: "ChId",
                principalTable: "ApprovalChange",
                principalColumn: "ChId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalGP_ApprovalChange_ChId",
                table: "PersonalGP");

            migrationBuilder.DropIndex(
                name: "IX_PersonalGP_ChId",
                table: "PersonalGP");

            migrationBuilder.DropColumn(
                name: "ChId",
                table: "PersonalGP");
        }
    }
}
