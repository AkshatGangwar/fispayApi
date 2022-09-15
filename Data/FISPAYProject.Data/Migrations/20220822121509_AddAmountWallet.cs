using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FISPAYProject.Data.Migrations
{
    public partial class AddAmountWallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "PassbookSummary",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "PassbookSummary",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "PassbookSummary");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "PassbookSummary",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
