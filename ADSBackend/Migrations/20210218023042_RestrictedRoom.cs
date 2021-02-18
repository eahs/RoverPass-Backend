using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ADSBackend.Migrations
{
    public partial class RestrictedRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RestrictedRoom",
                columns: table => new
                {
                    RestrictedRoomId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    IssuedDate = table.Column<DateTime>(nullable: false),
                    ClassId = table.Column<int>(nullable: false),
                    RoomNumberId = table.Column<int>(nullable: false),
                    RestrictionType = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestrictedRoom", x => x.RestrictedRoomId);
                    table.ForeignKey(
                        name: "FK_RestrictedRoom_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestrictedRoom_Class_RoomNumberId",
                        column: x => x.RoomNumberId,
                        principalTable: "Class",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestrictedRoom_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestrictedRoom_ClassId",
                table: "RestrictedRoom",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_RestrictedRoom_RoomNumberId",
                table: "RestrictedRoom",
                column: "RoomNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_RestrictedRoom_UserId",
                table: "RestrictedRoom",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RestrictedRoom");
        }
    }
}
