using Microsoft.EntityFrameworkCore.Migrations;

namespace ADSBackend.Migrations
{
    public partial class Class1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "JoinCode",
                table: "Class",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Block",
                table: "Class",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 32);

            migrationBuilder.CreateTable(
                name: "Class1",
                columns: table => new
                {
                    TeacherName = table.Column<string>(maxLength: 32, nullable: false),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    Block = table.Column<string>(maxLength: 32, nullable: false),
                    JoinCode = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class1", x => x.TeacherName);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Class1");

            migrationBuilder.AlterColumn<string>(
                name: "JoinCode",
                table: "Class",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<int>(
                name: "Block",
                table: "Class",
                type: "int",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 32);
        }
    }
}
