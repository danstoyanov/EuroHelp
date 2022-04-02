using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EuroHelp.Data.Migrations
{
    public partial class AddConsumerStatusColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Consumers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Consumers");
        }
    }
}
