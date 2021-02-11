using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ADSBackend.Migrations
{
    public partial class Restriction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restriction",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    staffNameId = table.Column<int>(nullable: true),
                    IssuedDate = table.Column<DateTime>(nullable: false),
                    ClassId = table.Column<int>(nullable: false),
                    restrictionType = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restriction", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Restriction_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Restriction_AspNetUsers_staffNameId",
                        column: x => x.staffNameId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restriction_ClassId",
                table: "Restriction",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Restriction_staffNameId",
                table: "Restriction",
                column: "staffNameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Restriction");
        }
    }
}
