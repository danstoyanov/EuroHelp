using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EuroHelp.Data.Migrations
{
    public partial class AddToConsumersUserIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Consumers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_UserId",
                table: "Consumers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_AspNetUsers_UserId",
                table: "Consumers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_AspNetUsers_UserId",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_UserId",
                table: "Consumers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Consumers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
