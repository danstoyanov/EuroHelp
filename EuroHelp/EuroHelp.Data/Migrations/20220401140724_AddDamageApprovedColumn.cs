using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EuroHelp.Data.Migrations
{
    public partial class AddDamageApprovedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsApproved",
                table: "Damages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Damages");
        }
    }
}
