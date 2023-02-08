using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideogameShop.Migrations
{
    /// <inheritdoc />
    public partial class WhoWannaChatReborn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messaggi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titolo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MittenteId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DestinatarioId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataOra = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TestoMessaggio = table.Column<string>(type: "text", nullable: false),
                    MessaggioLetto = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messaggi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messaggi_AspNetUsers_DestinatarioId",
                        column: x => x.DestinatarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messaggi_AspNetUsers_MittenteId",
                        column: x => x.MittenteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messaggi_DestinatarioId",
                table: "Messaggi",
                column: "DestinatarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Messaggi_MittenteId",
                table: "Messaggi",
                column: "MittenteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messaggi");
        }
    }
}
