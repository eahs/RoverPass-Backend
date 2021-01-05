using Microsoft.EntityFrameworkCore.Migrations;

namespace ADSBackend.Migrations
{
    public partial class AddedPeriod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Block",
                table: "Class");

            migrationBuilder.AddColumn<int>(
                name: "PeriodId",
                table: "Class",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Period",
                columns: table => new
                {
                    PeriodId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Period", x => x.PeriodId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Class_PeriodId",
                table: "Class",
                column: "PeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_Period_PeriodId",
                table: "Class",
                column: "PeriodId",
                principalTable: "Period",
                principalColumn: "PeriodId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Class_Period_PeriodId",
                table: "Class");

            migrationBuilder.DropTable(
                name: "Period");

            migrationBuilder.DropIndex(
                name: "IX_Class_PeriodId",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "PeriodId",
                table: "Class");

            migrationBuilder.AddColumn<int>(
                name: "Block",
                table: "Class",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
