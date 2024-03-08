using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GatePassManagementSystem.Migrations
{
    public partial class AddNRtrnGPToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NonReturnableGP",
                columns: table => new
                {
                    NonReturnableGPId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepId = table.Column<int>(type: "int", nullable: false),
                    AShod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ASdgm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ASgm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ASmd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ASguard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChkifDeptHeadUn = table.Column<bool>(type: "bit", nullable: false),
                    Satisfy = table.Column<bool>(type: "bit", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HODRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromLocation = table.Column<int>(type: "int", nullable: false),
                    ToLocation = table.Column<int>(type: "int", nullable: false),
                    AcknoledgedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OutTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AShodtime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ASdgmtime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ASgmtime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ASmdtime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SinIntime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SinOuttime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RejctReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChApprvlId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonReturnableGP", x => x.NonReturnableGPId);
                    table.ForeignKey(
                        name: "FK_NonReturnableGP_Department_DepId",
                        column: x => x.DepId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_NonReturnableGP_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NonReturnableGP_DepId",
                table: "NonReturnableGP",
                column: "DepId");

            migrationBuilder.CreateIndex(
                name: "IX_NonReturnableGP_UserId",
                table: "NonReturnableGP",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NonReturnableGP");
        }
    }
}
