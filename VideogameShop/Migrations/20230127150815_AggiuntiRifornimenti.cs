using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideogameShop.Migrations
{
    /// <inheritdoc />
    public partial class AggiuntiRifornimenti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantitaDisponibile",
                table: "Videogiochi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rifornimenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VideogiocoId = table.Column<int>(type: "int", nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false),
                    NomeFornitore = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezzo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rifornimenti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rifornimenti_Videogiochi_VideogiocoId",
                        column: x => x.VideogiocoId,
                        principalTable: "Videogiochi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rifornimenti_VideogiocoId",
                table: "Rifornimenti",
                column: "VideogiocoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rifornimenti");

            migrationBuilder.DropColumn(
                name: "QuantitaDisponibile",
                table: "Videogiochi");
        }
    }
}
