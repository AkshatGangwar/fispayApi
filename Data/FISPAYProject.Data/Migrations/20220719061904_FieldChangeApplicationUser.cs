using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FISPAYProject.Data.Migrations
{
    public partial class FieldChangeApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mobile",
                table: "AspNetUsers",
                newName: "UserEmail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "AspNetUsers",
                newName: "Mobile");
        }
    }
}
