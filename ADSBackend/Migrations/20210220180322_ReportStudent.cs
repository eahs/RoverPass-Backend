using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ADSBackend.Migrations
{
    public partial class ReportStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportStudent",
                columns: table => new
                {
                    ReportId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    NameId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportStudent", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_ReportStudent_ReportType_NameId",
                        column: x => x.NameId,
                        principalTable: "ReportType",
                        principalColumn: "ReportTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportStudent_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportStudent_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportStudent_NameId",
                table: "ReportStudent",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportStudent_StudentId",
                table: "ReportStudent",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportStudent_UserId",
                table: "ReportStudent",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportStudent");
        }
    }
}
