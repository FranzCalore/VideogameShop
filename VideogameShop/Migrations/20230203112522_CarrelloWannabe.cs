using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideogameShop.Migrations
{
    /// <inheritdoc />
    public partial class CarrelloWannabe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarrelloId",
                table: "Acquisto",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Carrelli",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeUtente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataOra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrezzoTotale = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrelli", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acquisto_CarrelloId",
                table: "Acquisto",
                column: "CarrelloId");

            migrationBuilder.AddForeignKey(
                name: "FK_Acquisto_Carrelli_CarrelloId",
                table: "Acquisto",
                column: "CarrelloId",
                principalTable: "Carrelli",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acquisto_Carrelli_CarrelloId",
                table: "Acquisto");

            migrationBuilder.DropTable(
                name: "Carrelli");

            migrationBuilder.DropIndex(
                name: "IX_Acquisto_CarrelloId",
                table: "Acquisto");

            migrationBuilder.DropColumn(
                name: "CarrelloId",
                table: "Acquisto");
        }
    }
}
