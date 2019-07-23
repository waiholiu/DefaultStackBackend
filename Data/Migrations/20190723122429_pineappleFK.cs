using Microsoft.EntityFrameworkCore.Migrations;

namespace mvcWithAuth.Data.Migrations
{
    public partial class pineappleFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Pineapples",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pineapples_ApplicationUserId",
                table: "Pineapples",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pineapples_AspNetUsers_ApplicationUserId",
                table: "Pineapples",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pineapples_AspNetUsers_ApplicationUserId",
                table: "Pineapples");

            migrationBuilder.DropIndex(
                name: "IX_Pineapples_ApplicationUserId",
                table: "Pineapples");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Pineapples");
        }
    }
}
