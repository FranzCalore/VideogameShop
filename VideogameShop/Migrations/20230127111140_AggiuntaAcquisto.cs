using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideogameShop.Migrations
{
    /// <inheritdoc />
    public partial class AggiuntaAcquisto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acquisto",
                columns: table => new
                {
                    AcquistoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataAcquisto = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VideogiocoId = table.Column<int>(type: "int", nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acquisto", x => x.AcquistoId);
                    table.ForeignKey(
                        name: "FK_Acquisto_Videogiochi_VideogiocoId",
                        column: x => x.VideogiocoId,
                        principalTable: "Videogiochi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acquisto_VideogiocoId",
                table: "Acquisto",
                column: "VideogiocoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acquisto");
        }
    }
}
