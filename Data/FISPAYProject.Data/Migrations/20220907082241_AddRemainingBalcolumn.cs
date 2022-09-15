using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FISPAYProject.Data.Migrations
{
    public partial class AddRemainingBalcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RemainingBal",
                table: "PassbookSummary",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainingBal",
                table: "PassbookSummary");
        }
    }
}
