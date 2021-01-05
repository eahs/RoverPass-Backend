using Microsoft.EntityFrameworkCore.Migrations;

namespace ADSBackend.Migrations
{
    public partial class FixedClassProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JoinCode",
                table: "Class",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Class_JoinCode",
                table: "Class",
                column: "JoinCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Class_JoinCode",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "JoinCode",
                table: "Class");
        }
    }
}
