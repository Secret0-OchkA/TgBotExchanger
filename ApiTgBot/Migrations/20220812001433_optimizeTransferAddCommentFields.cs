using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTgBot.Migrations
{
    public partial class optimizeTransferAddCommentFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountId",
                table: "Transfers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ownerTransferId",
                table: "Transfers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_AmountId",
                table: "Transfers",
                column: "AmountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ownerTransferId",
                table: "Transfers",
                column: "ownerTransferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Accounts_ownerTransferId",
                table: "Transfers",
                column: "ownerTransferId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Currencies_AmountId",
                table: "Transfers",
                column: "AmountId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Accounts_ownerTransferId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Currencies_AmountId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_AmountId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_ownerTransferId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "AmountId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "ownerTransferId",
                table: "Transfers");
        }
    }
}
