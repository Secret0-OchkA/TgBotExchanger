using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiExchangeBot.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TelegramId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SetCurrency = table.Column<int>(type: "int", nullable: false),
                    GetCurrency = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Currencies_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ownerTransferId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountId = table.Column<int>(type: "int", nullable: false),
                    Confirmed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_Accounts_ownerTransferId",
                        column: x => x.ownerTransferId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transfers_Currencies_AmountId",
                        column: x => x.AmountId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Arguments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransferId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arguments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arguments_Transfers_TransferId",
                        column: x => x.TransferId,
                        principalTable: "Transfers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_TelegramId",
                table: "Accounts",
                column: "TelegramId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Arguments_TransferId",
                table: "Arguments",
                column: "TransferId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_AccountId",
                table: "Currencies",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_AmountId",
                table: "Transfers",
                column: "AmountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ownerTransferId",
                table: "Transfers",
                column: "ownerTransferId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arguments");

            migrationBuilder.DropTable(
                name: "ExchangeRates");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
