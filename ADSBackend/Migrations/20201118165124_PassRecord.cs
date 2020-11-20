using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ADSBackend.Migrations
{
    public partial class PassRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PassRecord",
                columns: table => new
                {
                    PassId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(nullable: true),
                    IsssuedDate = table.Column<DateTime>(nullable: false),
                    memberId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassRecord", x => x.PassId);
                    table.ForeignKey(
                        name: "FK_PassRecord_Member_memberId",
                        column: x => x.memberId,
                        principalTable: "Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PassRecord_memberId",
                table: "PassRecord",
                column: "memberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PassRecord");
        }
    }
}
