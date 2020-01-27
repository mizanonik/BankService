using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankServiceApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankMasters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    LastTransactionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankLedgers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BatchId = table.Column<int>(nullable: false),
                    BankMasterId = table.Column<int>(nullable: false),
                    TransactionTypeId = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankLedgers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankLedgers_BankMasters_BankMasterId",
                        column: x => x.BankMasterId,
                        principalTable: "BankMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankLedgers_TransactionTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "TypeName" },
                values: new object[] { 1, "Debit" });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "TypeName" },
                values: new object[] { 2, "Credit" });

            migrationBuilder.CreateIndex(
                name: "IX_BankLedgers_BankMasterId",
                table: "BankLedgers",
                column: "BankMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_BankLedgers_TransactionTypeId",
                table: "BankLedgers",
                column: "TransactionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankLedgers");

            migrationBuilder.DropTable(
                name: "BankMasters");

            migrationBuilder.DropTable(
                name: "TransactionTypes");
        }
    }
}
