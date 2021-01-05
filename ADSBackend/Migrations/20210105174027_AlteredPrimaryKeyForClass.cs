using Microsoft.EntityFrameworkCore.Migrations;

namespace ADSBackend.Migrations
{
    public partial class AlteredPrimaryKeyForClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Class",
                table: "Class");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherName",
                table: "Class",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "Class",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Class",
                table: "Class",
                column: "ClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Class",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Class");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherName",
                table: "Class",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Class",
                table: "Class",
                column: "TeacherName");
        }
    }
}
