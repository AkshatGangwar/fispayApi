using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FISPAYProject.Data.Migrations
{
    public partial class AddVendorAccountRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VendorAccountRegistration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IFSCCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountHolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorAccountRegistration", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendorAccountRegistration");
        }
    }
}
