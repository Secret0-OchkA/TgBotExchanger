using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTgBot.Migrations
{
    public partial class DelWallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Wallets_WalletId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Wallets_WalletId",
                table: "Currencies");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_WalletId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "WalletId",
                table: "Currencies",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Currencies_WalletId",
                table: "Currencies",
                newName: "IX_Currencies_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Accounts_AccountId",
                table: "Currencies",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Accounts_AccountId",
                table: "Currencies");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Currencies",
                newName: "WalletId");

            migrationBuilder.RenameIndex(
                name: "IX_Currencies_AccountId",
                table: "Currencies",
                newName: "IX_Currencies_WalletId");

            migrationBuilder.AddColumn<int>(
                name: "WalletId",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_WalletId",
                table: "Accounts",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Wallets_WalletId",
                table: "Accounts",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Wallets_WalletId",
                table: "Currencies",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id");
        }
    }
}
