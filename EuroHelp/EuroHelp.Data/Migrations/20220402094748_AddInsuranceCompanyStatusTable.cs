using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EuroHelp.Data.Migrations
{
    public partial class AddInsuranceCompanyStatusTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "InsuranceCompanies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "InsuranceCompanies");
        }
    }
}
