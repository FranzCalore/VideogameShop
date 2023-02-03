using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideogameShop.Migrations
{
    /// <inheritdoc />
    public partial class CarrelloWannabe3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acquisto_Carrelli_CarrelloId",
                table: "Acquisto");

            migrationBuilder.DropIndex(
                name: "IX_Acquisto_CarrelloId",
                table: "Acquisto");

            migrationBuilder.DropColumn(
                name: "CarrelloId",
                table: "Acquisto");

            migrationBuilder.CreateTable(
                name: "AcquistoCarrello",
                columns: table => new
                {
                    CarrelliId = table.Column<int>(type: "int", nullable: false),
                    ProdottiAcquistatiAcquistoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcquistoCarrello", x => new { x.CarrelliId, x.ProdottiAcquistatiAcquistoId });
                    table.ForeignKey(
                        name: "FK_AcquistoCarrello_Acquisto_ProdottiAcquistatiAcquistoId",
                        column: x => x.ProdottiAcquistatiAcquistoId,
                        principalTable: "Acquisto",
                        principalColumn: "AcquistoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcquistoCarrello_Carrelli_CarrelliId",
                        column: x => x.CarrelliId,
                        principalTable: "Carrelli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcquistoCarrello_ProdottiAcquistatiAcquistoId",
                table: "AcquistoCarrello",
                column: "ProdottiAcquistatiAcquistoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcquistoCarrello");

            migrationBuilder.AddColumn<int>(
                name: "CarrelloId",
                table: "Acquisto",
                type: "int",
                nullable: true);

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
    }
}
