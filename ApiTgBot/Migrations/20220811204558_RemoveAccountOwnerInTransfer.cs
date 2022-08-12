using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTgBot.Migrations
{
    public partial class RemoveAccountOwnerInTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Accounts_AccountOwnerId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_AccountOwnerId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "AccountOwnerId",
                table: "Transfers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountOwnerId",
                table: "Transfers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_AccountOwnerId",
                table: "Transfers",
                column: "AccountOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Accounts_AccountOwnerId",
                table: "Transfers",
                column: "AccountOwnerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
