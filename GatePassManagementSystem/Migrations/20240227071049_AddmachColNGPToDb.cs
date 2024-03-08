using Microsoft.EntityFrameworkCore.Migrations;

namespace GatePassManagementSystem.Migrations
{
    public partial class AddmachColNGPToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DrHelname",
                table: "NonReturnableGP",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MachineName",
                table: "NonReturnableGP",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MachineNo",
                table: "NonReturnableGP",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "NonReturnableGP",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleNo",
                table: "NonReturnableGP",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrHelname",
                table: "NonReturnableGP");

            migrationBuilder.DropColumn(
                name: "MachineName",
                table: "NonReturnableGP");

            migrationBuilder.DropColumn(
                name: "MachineNo",
                table: "NonReturnableGP");

            migrationBuilder.DropColumn(
                name: "Other",
                table: "NonReturnableGP");

            migrationBuilder.DropColumn(
                name: "VehicleNo",
                table: "NonReturnableGP");
        }
    }
}
