using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EuroHelp.Data.Migrations
{
    public partial class RemoveDamageImg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DamageImage",
                table: "Damages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DamageImage",
                table: "Damages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
