using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FISPAYProject.Data.Migrations
{
    public partial class usertypefieldchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserType",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
