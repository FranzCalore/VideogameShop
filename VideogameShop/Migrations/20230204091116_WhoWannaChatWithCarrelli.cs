using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideogameShop.Migrations
{
    /// <inheritdoc />
    public partial class WhoWannaChatWithCarrelli : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataOra",
                table: "Messaggi",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "MessaggioLetto",
                table: "Messaggi",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CarrelloInviato",
                table: "Carrelli",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessaggioLetto",
                table: "Messaggi");

            migrationBuilder.DropColumn(
                name: "CarrelloInviato",
                table: "Carrelli");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataOra",
                table: "Messaggi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
