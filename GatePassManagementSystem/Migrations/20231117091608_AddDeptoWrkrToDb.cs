using Microsoft.EntityFrameworkCore.Migrations;

namespace GatePassManagementSystem.Migrations
{
    public partial class AddDeptoWrkrToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeptId",
                table: "Workers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_DeptId",
                table: "Workers",
                column: "DeptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Department_DeptId",
                table: "Workers",
                column: "DeptId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Department_DeptId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_DeptId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "DeptId",
                table: "Workers");
        }
    }
}
