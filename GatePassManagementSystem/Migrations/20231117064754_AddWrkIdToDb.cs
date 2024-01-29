using Microsoft.EntityFrameworkCore.Migrations;

namespace GatePassManagementSystem.Migrations
{
    public partial class AddWrkIdToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WrkId",
                table: "WorkerGP",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorkerGP_WrkId",
                table: "WorkerGP",
                column: "WrkId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerGP_Workers_WrkId",
                table: "WorkerGP",
                column: "WrkId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerGP_Workers_WrkId",
                table: "WorkerGP");

            migrationBuilder.DropIndex(
                name: "IX_WorkerGP_WrkId",
                table: "WorkerGP");

            migrationBuilder.DropColumn(
                name: "WrkId",
                table: "WorkerGP");
        }
    }
}
