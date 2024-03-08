using Microsoft.EntityFrameworkCore.Migrations;

namespace GatePassManagementSystem.Migrations
{
    public partial class AddColItemDescToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NonGPId",
                table: "NonReturnItemDsc",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NonGPId",
                table: "NonReturnItemDsc");
        }
    }
}
