using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FISPAYProject.Data.Migrations
{
    public partial class AddWallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PassbookSummary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TxnId = table.Column<long>(type: "bigint", nullable: false),
                    TxnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Mode = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassbookSummary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonWalletRegistration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PanNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsWalletActivate = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonWalletRegistration", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PassbookSummary");

            migrationBuilder.DropTable(
                name: "PersonWalletRegistration");
        }
    }
}
