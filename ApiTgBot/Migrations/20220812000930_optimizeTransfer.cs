using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTgBot.Migrations
{
    public partial class optimizeTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Currencies_AmountId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_AmountId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "AmountId",
                table: "Transfers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountId",
                table: "Transfers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_AmountId",
                table: "Transfers",
                column: "AmountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Currencies_AmountId",
                table: "Transfers",
                column: "AmountId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
