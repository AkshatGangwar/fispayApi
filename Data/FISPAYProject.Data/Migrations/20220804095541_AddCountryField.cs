using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FISPAYProject.Data.Migrations
{
    public partial class AddCountryField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Zip_Pincode",
                table: "AspNetUsers",
                newName: "Zipcode");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Landmark",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Landmark",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StoreName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Zipcode",
                table: "AspNetUsers",
                newName: "Zip_Pincode");
        }
    }
}
