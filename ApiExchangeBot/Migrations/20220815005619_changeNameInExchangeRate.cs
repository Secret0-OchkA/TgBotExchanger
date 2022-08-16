using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiExchangeBot.Migrations
{
    public partial class changeNameInExchangeRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SetCurrency",
                table: "ExchangeRates",
                newName: "outType");

            migrationBuilder.RenameColumn(
                name: "GetCurrency",
                table: "ExchangeRates",
                newName: "inType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "outType",
                table: "ExchangeRates",
                newName: "SetCurrency");

            migrationBuilder.RenameColumn(
                name: "inType",
                table: "ExchangeRates",
                newName: "GetCurrency");
        }
    }
}
