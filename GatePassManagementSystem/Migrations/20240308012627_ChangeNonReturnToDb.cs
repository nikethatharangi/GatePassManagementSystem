using Microsoft.EntityFrameworkCore.Migrations;

namespace GatePassManagementSystem.Migrations
{
    public partial class ChangeNonReturnToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "NonReturnItemDsc",
                newName: "NonReturnItemDscId");

            migrationBuilder.AddColumn<int>(
                name: "NonReturnItemDscId1",
                table: "NonReturnItemDsc",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NonReturnItemDsc_NonReturnItemDscId1",
                table: "NonReturnItemDsc",
                column: "NonReturnItemDscId1");

            migrationBuilder.AddForeignKey(
                name: "FK_NonReturnItemDsc_NonReturnItemDsc_NonReturnItemDscId1",
                table: "NonReturnItemDsc",
                column: "NonReturnItemDscId1",
                principalTable: "NonReturnItemDsc",
                principalColumn: "NonReturnItemDscId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NonReturnItemDsc_NonReturnItemDsc_NonReturnItemDscId1",
                table: "NonReturnItemDsc");

            migrationBuilder.DropIndex(
                name: "IX_NonReturnItemDsc_NonReturnItemDscId1",
                table: "NonReturnItemDsc");

            migrationBuilder.DropColumn(
                name: "NonReturnItemDscId1",
                table: "NonReturnItemDsc");

            migrationBuilder.RenameColumn(
                name: "NonReturnItemDscId",
                table: "NonReturnItemDsc",
                newName: "Id");
        }
    }
}
