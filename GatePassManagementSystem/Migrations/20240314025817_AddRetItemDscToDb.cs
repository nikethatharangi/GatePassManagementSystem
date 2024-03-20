using Microsoft.EntityFrameworkCore.Migrations;

namespace GatePassManagementSystem.Migrations
{
    public partial class AddRetItemDscToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReturnItemDsc",
                columns: table => new
                {
                    ReturnItemDscId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReturnableGPId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnItemDsc", x => x.ReturnItemDscId);
                    table.ForeignKey(
                        name: "FK_ReturnItemDsc_ReturnableGP_ReturnableGPId",
                        column: x => x.ReturnableGPId,
                        principalTable: "ReturnableGP",
                        principalColumn: "ReturnableGPId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReturnItemDsc_ReturnableGPId",
                table: "ReturnItemDsc",
                column: "ReturnableGPId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReturnItemDsc");
        }
    }
}
