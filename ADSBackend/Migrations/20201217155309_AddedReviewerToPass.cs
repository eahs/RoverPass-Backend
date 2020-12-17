using Microsoft.EntityFrameworkCore.Migrations;

namespace ADSBackend.Migrations
{
    public partial class AddedReviewerToPass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewerId",
                table: "Pass",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pass_ReviewerId",
                table: "Pass",
                column: "ReviewerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pass_AspNetUsers_ReviewerId",
                table: "Pass",
                column: "ReviewerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pass_AspNetUsers_ReviewerId",
                table: "Pass");

            migrationBuilder.DropIndex(
                name: "IX_Pass_ReviewerId",
                table: "Pass");

            migrationBuilder.DropColumn(
                name: "ReviewerId",
                table: "Pass");
        }
    }
}
