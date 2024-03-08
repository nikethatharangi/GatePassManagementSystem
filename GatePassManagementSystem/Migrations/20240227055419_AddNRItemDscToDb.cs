using Microsoft.EntityFrameworkCore.Migrations;

namespace GatePassManagementSystem.Migrations
{
    public partial class AddNRItemDscToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NonReturnItemDsc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NonReturnableGPId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonReturnItemDsc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NonReturnItemDsc_NonReturnableGP_NonReturnableGPId",
                        column: x => x.NonReturnableGPId,
                        principalTable: "NonReturnableGP",
                        principalColumn: "NonReturnableGPId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NonReturnItemDsc_NonReturnableGPId",
                table: "NonReturnItemDsc",
                column: "NonReturnableGPId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NonReturnItemDsc");
        }
    }
}
